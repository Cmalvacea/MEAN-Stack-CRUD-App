const Patient = require("./Schemas/PatientSchema")
const mongoose = require("mongoose")
const express = require('express')
const cors = require('cors')
const {customAlphabet} = require('nanoid')
const PIDGEN = customAlphabet("123456789", 7)
const { json } = require("express")
const dotenv = require('dotenv').config({path: "../.env"})
const app = express()
const PORT = process.env.BACK1PORT;
const DB_URI = process.env.DB_URI


app.use(express.json())
app.use(express.urlencoded({extended: true}))
app.use(cors({
    origin: `http://127.0.0.1:${process.env.FRONTPORT}`
}))

function Connect() {
    try {
        mongoose.connect(DB_URI)
    } catch (error) {
        console.log("Could not connect to the database")
    }
}
Connect()


app.route("/Patients")
.get(async (req, res) => {
    var MyList = await Patient.find({})
    if(!MyList) {
        res.status(404)
    } else {
        res.status(200).json(MyList)
    }
})
.post(async (req, res) => {
    var NewPatient = await new Patient({
        PersonalID: PIDGEN(),
        Name: req.body.name,
        Age: req.body.age,
        Weight: `${req.body.weight} Kg`,
        Height: `${req.body.height} Cm`,
        Gender: `${req.body.gender}`,
        Diagnosis: req.body.diagnosis
    })
    await NewPatient.save((err, data) => {
        if(err) throw err, res.status(404);
        res.status(200).send("The record has been saved succesfully")
    })
})
.put(async (req, res) => {
    var Filter = {"PersonalID": req.body.id}
    var Update = {"Diagnosis": req.body.newdiag}
    var NewRecord = await Patient.updateOne(Filter, Update, {new: true})
    if(!NewRecord) {
        res.status(404)
    } else {
        res.status(201).end("The patient has been updated")
    }

})
.delete(async (req, res) => {
    var Filter = {"PersonalID": req.body.id}
    var check = await Patient.findOne(Filter)
    if(!check) {
        res.status(404)
    } else {
        await Patient.deleteOne(Filter)
        res.status(200).end("Patient deleted")
    }
})



app.listen(PORT, () => {
    console.log(`Listening at port ${PORT}`)
})
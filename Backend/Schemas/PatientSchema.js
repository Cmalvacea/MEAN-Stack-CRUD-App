const mongoose = require("mongoose")

const PatientSchema = new mongoose.Schema({
    PersonalID: String,
    Name: {
        type: String
    },
    Age: Number,
    Weight: String,
    Height: String,
    Gender: String,
    Diagnosis: {
        type: String,
        default: "Healthy"
    }
})

const Patient = mongoose.model("Patient", PatientSchema)

module.exports = Patient
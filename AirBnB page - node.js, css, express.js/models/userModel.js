//require the same models 
const mongoose = require("mongoose");
const Schema = mongoose.Schema;

mongoose.Promise = require("bluebird");
//when we use this schema, it's already set up for promise

const userSchema = new Schema({
    "email": {
        type: String,
        required: true,
        unique: true
    },
    "password": {
        type: String,
        required: true,
        minlength : 8
    },
    "fname": {
        type: String,
        required: true
    },
    "lname": {
        type: String,
        required: true
    },
    "phone": {
        type: Number,
        required: true,
        minlength: 10
    },
    "bday": {
        type: Date,
        required: true,
        default: Date.now
    },
    "isAdmin": {
        type: Boolean,
        default: false
    }
});

module.exports = mongoose.model("test-airbnb", userSchema);
//what we will return is mongoose models with userSchema
const mongoose = require("mongoose");
const Schema = mongoose.Schema;

mongoose.Promise = require("bluebird");
//when we use this schema, it's already set up for promise

const bookedSchema = new Schema({
    "title": String,
    "total": Number,
    "start": Date,
    "end": Date,
    "days": Number,
    "guest": Number
});

module.exports = mongoose.model("test-booked", bookedSchema);
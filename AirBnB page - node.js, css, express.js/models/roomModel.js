const mongoose = require("mongoose");
const Schema = mongoose.Schema;

mongoose.Promise = require("bluebird");
//when we use this schema, it's already set up for promise

const roomSchema = new Schema({
    "_id":{
        type: Number,
        min: 1,
        max: 6
    },
    "title": String,
    "price": Number,
    "desc": String,
    "location": String,
    "roomImg": String,
    "rate": [false, false, false, false, false],
    "review": Number
});

module.exports = mongoose.model("test-room", roomSchema);
var express = require("express");
var app = express();
var path=require("path");
const hbs = require('express-handlebars');
var bodyParser = require("body-parser");
var nodemailer = require("nodemailer");
const mongoose = require("mongoose");
const fs = require("fs");
const UsrModel = require("./models/userModel");
const roomModel = require("./models/roomModel");
const bookedModel = require("./models/bookedModel");
const clientSessions = require ("client-sessions");
const { runInNewContext } = require("vm");
mongoose.Promise = require("bluebird");
const { response } = require("express");
var helpers = require('handlebars-helpers')();

//=====================credential for mongodb==================
var dotenv = require('dotenv');
dotenv.config();
const connectionString = `mongodb+srv://${process.env.DB_USERNAME}:${process.env.DB_PASSWORD}@senecaweb.phalr.mongodb.net/web322_week8?retryWrites=true&w=majority`;
//==============================================================

var HTTP_PORT = process.env.PORT || 8080;

var transpoter = nodemailer.createTransport({
    service: "gmail",
    auth: {
        user: "gmlqls456123@gmail.com",
        pass: "wkdakak12."
    }
});

app.engine('.hbs', hbs({extname: '.hbs',
    runtimeOptions: {
        allowProtoPropertiesByDefault: true,
        allowProtoMethodsByDefault: true
}}));
app.set('view engine', '.hbs');

function onHttpStartup(){
    console.log("Express Server running on port "+HTTP_PORT);
}

app.use(express.static(__dirname + "/views"));
app.use(express.json());
app.use(bodyParser.urlencoded({ extended: false }));
//app.use(passport.initialize());
//app.use(passport.session());

//========================connect to db========================
mongoose.connect(connectionString, { useNewUrlParser: true, useUnifiedTopology: true });
mongoose.set('useCreateIndex', true);
mongoose.connection.on("open", () => {
    console.log("Database connection open.");
  });
//================================================================

//========================encrypt password=========================
var bcrypt = require('bcryptjs');

//===================================================================

//=================== stay logging in / log out ================
//set the timer 
app.use(clientSessions({
    cookieName: "session",
    secret: "authentication",
    duration: 5*60*1000, //5mins
    //everytime you wanna run this, how long is gonna be active for?
    activeDuration: 1000*60 // 1mins expired if we talk too much?
}));

function ensureLogin(req, res, next) {
    if (!req.session.user) {
      res.redirect("/login");
    } else {
      next();
    }
  };
//========================================================

app.get("/", function(req, res){
    res.render("home",{user: req.session.user, layout: false});
});
app.get("/dashboard2", ensureLogin, (req, res)=>{
    res.render("dashboard2", {user: req.session.user, layout: false});
});
app.get("/login", (req, res)=>{
    res.render("login", {layout: false});
});
app.post("/login", (req, res)=>{
    const email = req.body.useremail;
    const password = req.body.userpassword;
    //userpassword = bcrypt.hashSync(userpassword);

    if(email == "" || password == "")
        return res.render("login", {errorMsg: "Both fields are required !", layout: false});
    
    
    UsrModel.findOne({email: email})
    .exec()
    .then((usr)=>{
        if(!usr){
            res.render("login", {errorMsg: "Login dose not exists!!", layout: false});
        }
        else{
            if(email === usr.email){
                // compare the hashed password
                bcrypt.compare(password, usr.password, (err, tf) => {
                if (tf !== true) {//if the comparison is not true
                    res.render("login", {errorMsg: "login and password does not match !", layout: false});
                }
                else{
                    req.session.user = {
                        email: usr.email,
                        fname: usr.fname,
                        lname: usr.lname,
                        bday: usr.bday,
                        phone: usr.phone,
                        isAdmin: usr.isAdmin
                    };
                    res.redirect("/dashboard2"); 
                }
                })
            }
        }
    })
    .catch((err)=>{console.log("An error occurred:  ${err}")});
});

app.get("/logout", (req, res)=>{
    req.session.reset();
    res.redirect("/");
});
//============================profile edit===================
app.get("/profile", ensureLogin, (req,res)=>{
    res.render("profile", {user: req.session.user, layout: false});
 });
 app.get("/profile/edit", ensureLogin, (req,res)=>{
    res.render("profileEdit", {user: req.session.user, layout: false});
});
app.post("/profile/edit", ensureLogin, (req,res) => {
    const email = req.body.email;
    var pw = req.body.password;
    const phone = req.body.phone;
    pw = bcrypt.hashSync(pw);

    UsrModel.updateOne(
        { email: email },
        {$set: {
            email: email,
            password: pw,
            phone: phone
        }}
    ).exec()
    .then(()=>{
        UsrModel.findOne({email: email})
        .exec()
        .then((usr)=>{
            req.session.user = {
            email: email,
            fname: usr.fname,
            lname: usr.lname,
            bday: usr.bday,
            phone: phone,
            isAdmin: usr.isAdmin
        };
        res.redirect("/profile");
    });
    });
});

/* =========================room desc, room booking========================= */

app.get("/room/book", ensureLogin, function(req, res){
    res.render("roomDesc", {user: req.session.user, layout: false});
});
app.get("/room/book/:roomid", ensureLogin, (req, res) => {
    const roomid = req.params.roomid;

    roomModel.findOne({_id: roomid})
        .lean()
        .exec()
        .then((room)=>{
            bookedModel.findOne({title: room.title})
            .lean()
            .exec()
            .then((booked)=>{
                res.render("roomDesc", {booked: booked, user: req.session.user, room: room, layout: false})
            })
            .catch((err)=>{});
        });
});
app.get("/confirmation", ensureLogin, (req, res)=>{
    res.render('confirmation', {user: req.session.user, layout: false});
});
app.post("/room/book", ensureLogin, (req,res) => {
//as there was an time difference of the date from js in handlebars, I added timezone offset
    var start = new Date(req.body.checkin);
    start.setTime(start.getTime()+start.getTimezoneOffset()*60*1000);
    var end = new Date(req.body.checkout);
    end.setTime(end.getTime()+end.getTimezoneOffset()*60*1000)
    var diffTime = end.getTime()-start.getTime();
    const days = diffTime / (1000 * 3600 * 24);
//need to make an object named booked
    const id = req.body.idNum;

    var formatStart = start.getUTCFullYear()+"/"+(start.getUTCMonth() + 1)+"/"+start.getUTCDate();
    var formaEnd = end.getUTCFullYear()+"/"+(end.getUTCMonth() + 1)+"/"+end.getUTCDate();

    roomModel.findOne({_id: id})
    //find room which users want to book by id
    .lean()
    .exec()
    .then((room)=>{
        var price = days * (room.price);
        var total = price * 1.13;
        total = Math.round(total*100)/100;

        const booked = new bookedModel({
            title: room.title,
            total: total,
            start: start,
            end: end,
            days: days,
            guest: req.body.guests
        });

        //start< enddate & end > startdate
       //an arrya contatining all possible overlaps
       bookedModel.find({title: room.title})
       .and([
           {start: {"$lt": end}},
           {end: {"$gt": start}}
        ])
        .exec(function(err, result){
            if(result.length === 0) //cant find
            {
                booked.save()
                .then((response)=>{
                var mailOption = {
                    from: "gmlqls456123@gmail.com",
                    to: req.session.user.email,
                    subject: "Confirmation of your booking with details",
                    html: "<p>Hello "+ req.session.user.fname + '</p><p>Thank you for using our service.</p>' +
                    "<p>The " +  room.title + "</p>"+
                    '<p>booked from ' + formatStart +" to "+
                    formaEnd + " for " + days + "days</p>"+
                    "<p>The total price is <b>&#36;"+ total +"</b> for "+
                    req.body.guests +" guest(s)</p>"
                }
                transpoter.sendMail(mailOption, (error, info)=>{
                    if(error){
                    console.log("ERROR: " + error);
                    }else{
                    console.log("SUCCESS: " + info.response);
                }
                });
                res.render("confirmation", {booked: booked , user: req.session.user, room: room, layout: false});
                })
            }
            else
            res.render("roomDesc", {user: req.session.user, room: room, bookingMsg: "This room is already booked between those dates", layout: false});
        });
    });
});
//===========================rooms edit================


app.get("/rooms", function(req, res){
    roomModel.find()//go get everything (no {})
    .lean() //convert to the js object which handlebars can read
    .exec()
    .then((room)=>{
        res.render("rooms", {room: room, hasRooms: !!room.length, user: req.session.user, layout: false});
    });
});
app.get("/room/edit", ensureLogin, (req,res) => {
    res.render("roomEdit", {user: req.session.user, layout: false});
});
app.get("/room/edit/:roomid", ensureLogin, (req, res) => {
    const roomid = req.params.roomid;

    roomModel.findOne({_id: roomid})
        .lean()
        .exec()
        .then((room)=>{
            res.render("roomEdit", {user: req.session.user, room: room, editmode: true, layout: false});
        });
});

app.get("/room/delete/:roomid", ensureLogin, (req, res) => {
    const roomid = req.params.roomid;
    roomModel.deleteOne({_id: roomid})
        .then(()=>{
            res.redirect("/rooms");
        });
})
app.post("/room/edit", ensureLogin, (req,res) => {
    var arr=[];
    for(var i = 0; i<req.body.rate; i++)
        arr[i] = true;
    
    const room = new roomModel({
        _id: req.body.ID,
        title: req.body.title,
        price: req.body.price,
        desc: req.body.desc,
        location: req.body.roomLocation,
        roomImg: req.body.roomImg,
        rate: arr,
        review: req.body.review
    });

    if (req.body.edit === "1") {
        // editing
        roomModel.updateOne({_id: room._id},
            { $set: {
                title: room.title,
                price: room.price,
                desc: room.desc,
                location: room.location,
                roomImg: room.roomImg,
                rate: room.rate,
                review: room.review
            }}
            ).exec().then((err)=>{});
           
         //car.updateOne((err)=>{});

    } else { 
        //adding
        room.save((err)=>{});
    };
    // render is matter. redirect is ok
    res.redirect("/rooms");

});
/*=========================room search==================================== */
app.post("/roomSearch", function(req, res){
    const location = req.body.locationSearch;
    roomModel.find({location: location})
    .exec(function(err, result){
        if(result.length === 0) //cant find
        {
            res.render("rooms", {hasRooms: !!result.length, user: req.session.user, layout: false});
        }    
        else{
            res.render("rooms", {hasRooms: !!result.length, room: result, user: req.session.user, layout: false});
        }
    });
});

/* ============registration======================== */
app.get("/registration", function(req, res){
    res.render("registration", {user: req.session.user, layout: false});
});


app.post("/registration", (req, res)=> 
//as the form method is post, we gonna use post, the pjoto is name not id
{
    const FORMDATA = req.body;
    //variable for password
    let pw = FORMDATA.password;
    //encrypt the password in pw
    pw = bcrypt.hashSync(pw);

    var mailOption = {
            from: "gmlqls456123@gmail.com",
            to: FORMDATA.email,
            subject: "Confirmation of your registration from our Airbnb web site !",
            html: "<p>Hello "+ FORMDATA.fname + '<p>Thank you for contacting us.</p>' +
                '<p>Click <a href="https://calm-temple-50612.herokuapp.com/">here</a> to visit our website</p>'
    }

    transpoter.sendMail(mailOption, (error, info)=>{
        if(error){
            console.log("ERROR: " + error);
        }else{
            console.log("SUCCESS: " + info.response);
        }
    });

    const locals = { 
        message: "Your information was uploaded successfully !!",
        layout: false 
      };
      var admin;
    if(FORMDATA.email==="gmlqls456@naver.com")
        admin = true;
    else
        admin = false;
    const userMetadata = new UsrModel({
        email: FORMDATA.email,
        password: pw,
        fname: FORMDATA.fname,
        lname: FORMDATA.lname,
        phone: FORMDATA.phone,
        bday: new Date(FORMDATA.bday),
        isAdmin: admin
      });
      UsrModel.countDocuments({email: userMetadata.email}, (err, count)=>{
        if(count>0){
            locals.message = "The email address already exists !!";    
            res.render("registration", {Msg: locals.message, layout: false});
        }else{
            userMetadata.save()
            .then((response) => {
                res.render("registration", {Msg: locals.message, layout: false});

                app.get("/dashboard", function(requ, resp){
                var someData={
                fname: FORMDATA.fname,
                lname: FORMDATA.lname
                }
                resp.render('dashboard', {data: someData, Msg: locals.message, layout: false});
                });
                return res.redirect("dashboard");
            })
            .catch((err) => {
            locals.message = "There was an error uploading your information !!";
    
            res.render("registration", {Msg: locals.message, layout: false});
            });
            }
        });    
    
});




app.listen(HTTP_PORT, onHttpStartup);

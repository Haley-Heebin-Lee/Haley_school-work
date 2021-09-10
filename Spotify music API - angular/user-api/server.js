const express = require('express');
const cors = require("cors");
const jwt = require('jsonwebtoken');
const passport = require("passport");
const passportJWT = require("passport-jwt");
const dotenv = require("dotenv");

dotenv.config();

const userService = require("./user-service.js");
const app = express();
//****************setting jwt & express *************** */
let ExtractJwt = passportJWT.ExtractJwt;
let JwtStrategy = passportJWT.Strategy;

let jwtOptions = {};
jwtOptions.jwtFromRequest = ExtractJwt.fromAuthHeaderWithScheme("jwt")
jwtOptions.secretOrKey = process.env.JWT_SECRET

let strategy = new JwtStrategy(jwtOptions, function (jwt_payload, next) {
    console.log('payload received', jwt_payload);

    if (jwt_payload) {
        // The following will ensure that all routes using 
        // passport.authenticate have a req.user._id, req.user.userName  
        // that matches the request payload data
        next(null, {
             _id: jwt_payload._id, 
            userName: jwt_payload.userName, 
           }); 
    } else {
        next(null, false);
    }
});
// tell passport to use our "strategy"
passport.use(strategy);
// add passport as application-level middleware
app.use(passport.initialize());

//************************************************************ */

const HTTP_PORT = process.env.PORT || 8080;

app.use(express.json());
app.use(cors());

/* TODO Add Your Routes Here */

app.post("/api/user/register", (req, res) => {
    userService.registerUser(req.body)
    .then((msg)=>{
        res.join({"message":msg});
    }).catch((msg)=>{
        res.status(422).json({"message":msg});
    })
});
app.post("/api/user/login", (req, res) => {
    userService.checkUser(req.body)
        .then((user) => {
            const payload = { 
                _id: user._id,
                userName: user.userName
            };

            const token = jwt.sign(payload, jwtOptions.secretOrKey);

            res.json({ "message": "login successful", "token": token });
        }).catch((msg) => {
            res.status(422).json({ "message": msg });
        });
});
app.get("/api/user/favourites", passport.authenticate('jwt', { session: false }), (req,res)=>{
    userService.getFavourites(req.user._id)
    .then((data)=>{
        res.json(data);
    }).catch((msg)=>{
        res.status(422).jason({"error": msg});
    });
});
 
app.put("/api/user/favourites/:id", passport.authenticate('jwt', { session: false }), (req,res)=>{
    userService.addFavourite(req.user._id, req.params.id)
    .then((data)=>{
        res.json(data);
    }).catch((msg)=>{
        res.status(422).jason({"error": msg});
    });
});
app.delete("/api/user/favourites/:id", passport.authenticate('jwt', { session: false }), (req,res)=>{
    userService.removeFavourite(req.user._id, req.params.id)
    .then((data)=>{
        res.json(data);
    }).catch((msg)=>{
        res.status(422).jason({"error": msg});
    });
});

userService.connect()
.then(() => {
    app.listen(HTTP_PORT, () => { console.log("API listening on: " + HTTP_PORT) });
})
.catch((err) => {
    console.log("unable to start the server: " + err);
    process.exit();
});
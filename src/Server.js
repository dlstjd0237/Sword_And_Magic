import express from 'express' //신버전  //const express = require("require"); 구버전 
import { Pool } from './DB.js'
import { UserRouter } from './Router/UserRouter.js';
import { LogRouter } from './Router/logRouter.js';
import { LoginChecker } from './Middleware/LoginChecker.js';
import { MSG, MsgType } from './Router/CommonPacket.js';
import nunjucks from "nunjucks";
import  JsonWebTokenError  from 'jsonwebtoken';

const app = express();

app.set("view engine", "njk");
nunjucks.configure("views", { express: app, watch: true });

app.use(express.json());
app.use(express.urlencoded({ extended: true }));
app.use(express.static('public'));

app.use(LoginChecker); //토큰 가져오는 작업을 수행한다.

app.get("/", (req, res) => {
    res.render("index");
});

app.get("/record", async (req, res) => {

    if (req.user == null) {
        res.redirect("/");
        return;
    }

    let sql = `     SELECT * 
                  FROM log_records AS l, users AS u 
                  WHERE l.user_id = u.id
                  ORDER BY record DESC`;
    let [rows, col] = await Pool.query(sql);

    rows = rows.map(x => {
        x.created = x.created.toLocaleString();
        return x;
    });

    res.render("record", { rows });
});


app.get("/game_rank", async (req, res) => {
    let sql = "SELECT u.name, c.time FROM users AS u, clear_records AS c WHERE u.id = c.user_id ORDER BY c.time DESC";
    let [rows, col] = await Pool.query(sql);

    MSG.type = MsgType.SUCCESS;
    MSG.msg = JSON.stringify({list:rows});
    
    console.log(MSG);
   
    res.json(MSG);
});

app.use(UserRouter);
app.use(LogRouter);

app.listen(4500, () => {
    console.log(
        `
        ###################################
        # Server is running on 4500 prot  #
        # http://localhost:4500/          #
        ###################################
        `);

});
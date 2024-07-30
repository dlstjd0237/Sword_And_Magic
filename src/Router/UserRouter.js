import {Router} from 'express';
import { MsgType, MSG } from './CommonPacket.js';
import { Pool } from '../DB.js';
import jwt from "jsonwebtoken";
import { PrivateKey } from '../Secret.js';

export const UserRouter = new Router();

UserRouter.post("/login", async(req, res) => {
    let {email, password} = req.body;
    
    let {result, token, user} = await loginProcess(email, password);

    if(result)
    {
        if(user.level < 99) {
            res.json({msg:"로그인 실패. 권한 없습니다."},403);//403 권한 없음

        }else{
            res.json({token,user});
        }
    }else{
        res.json({msg:"로그인 이메일 또는 비밀번호가 올바르지 않습니다."},401);
    }
});

UserRouter.post("/user_register", async (req, res) => {
    //무언가 요청을 받아서 넣어줘야겠지?
    let {email, name, password} = req.body;

    const sql = "INSERT INTO users (email, password, name) VALUES(?,SHA1(?),?)";
    try {
        let result = await Pool.query(sql, [email, password, name]);
        //console.log(result); //성공시에 affectedRows : 영향을 끼친 행 수 
        MSG.type = MsgType.SUCCESS;
        MSG.msg = "성공적으로 가입 완료";
        res.json(MSG);
    }catch(err) 
    {
        MSG.type = MsgType.ERROR;
        MSG.msg = "서버 오류 발생";
        res.json(MSG);
    }
    
});

async function loginProcess(email, password)
{
    const sql = "SELECT * FROM users WHERE email = ? AND password = SHA1(?)";
    let [row, col] = await Pool.query(sql, [email, password]);

    if(row.length == 1)  //로그인 성공한거
    {
        let {id, email, name} = row[0];
        let token = jwt.sign({id, email, name}, PrivateKey, {expiresIn:'7 days'});
        return {result : true, token, user:row[0]};
    }else {
        return {result : false, token:null, user:null};
    }
}

UserRouter.post("/user_login", async (req, res) => {
    let {email, password} = req.body;

    let {result, token} = await loginProcess(email, password);
    console.log(token);
    if(result){
        MSG.type = MsgType.SUCCESS;
        MSG.msg = token;
        res.json(MSG);
    }else{
        MSG.type = MsgType.ERROR;
        MSG.msg = "입력한 값이 올바르지 않습니다.";
        res.json(MSG);
    }
});

UserRouter.get("/verify_token", (req, res) => {
    let token = extractToken(req); //사용자가 요청한 토큰을 까서 가져오고

    try {
        let decodedToken = jwt.verify(token, PrivateKey);
        if(decodedToken != null)
        {
            MSG.type = MsgType.SUCCESS;
        }else {
            MSG.type = MsgType.ERROR;
        }
    }catch(err)
    {
        console.log(err);
        MSG.type = MsgType.ERROR;
    }
    res.json(MSG);
});

function extractToken(req)
{
    let prefix = "Bearer"; //요건 약속
    let auth = req.headers.authorization;
    let token = auth.includes(prefix) ? auth.split(prefix)[1] : auth;

    return token;
}
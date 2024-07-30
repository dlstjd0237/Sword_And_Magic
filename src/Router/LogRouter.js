import {Router} from 'express'
import { MsgType, MSG } from './CommonPacket.js';
import { Pool } from '../DB.js';
export const LogRouter = new Router();

LogRouter.post("/game_clear_log", async (req, res) => {
    if (req.user == null) {
        MSG.type = MsgType.ERROR;
        MSG.msg = "허가되지 않은 접근";
        res.json(MSG);
        return;
    }

    let { time } = req.body;
    let userId  = req.user.id;
    console.log(req.user.id);
    console.log(req.user);
    //여기서 분터는 직접 슛
    const sql = "INSERT INTO clear_records(user_id,time,created) VALUES (?,?,NOW())";
    try {
        let [result, column] = await Pool.query(sql, [userId, time]);
        if (result.affectedRows == 1) {
            MSG.type = MsgType.SUCCESS;
            MSG.msg = "성공적으로 기록";
        } else {
            MSG.type = MsgType.ERROR;
            MSG.msg = "기록 중 오류 발생";
        }
    } catch (err) {
        console.log(err);
    }

    res.json(MSG);

});

LogRouter.post("/game_reset_log", async (req, res) => {

    if (req.user == null) {
        MSG.type = MsgType.ERROR;
        MSG.msg = "허가되지 않은 접근";
        res.json(MSG);
        return;
    }
    let { record } = req.body;
    let userId = req.user.id;

    const sql = "INSERT INTO log_records(user_id,record,created) VALUES (?,?,NOW())";

    try {
        let [result, column] = await Pool.query(sql, [userId, record]);
        if (result.affectedRows == 1) {
            MSG.type = MsgType.SUCCESS;
            MSG.msg = "성공적으로 기록";
        } else {
            MSG.type = MsgType.ERROR;
            MSG.msg = "기록 중 오류 발생";
        }
    } catch (err) {
        console.log(err);
    }

    res.json(MSG);

});
import { combineReducers } from "redux";
import { member } from "./member";
import { messages } from "./message";
import { groups } from "./group";

export default combineReducers({ member, messages, groups });

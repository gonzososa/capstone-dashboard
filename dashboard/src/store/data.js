import axios from "axios";
import API from './config';
import parseList from "./action-utils";

export const getAllData = async () => {
    const response = await axios.get (`${API}/GetAllData`);
    return parseList (response);
}
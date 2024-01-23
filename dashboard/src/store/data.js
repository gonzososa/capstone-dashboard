import axios from "axios";
import API from './config';
import parseList from "./action-utils";

export const getAllData = async () => {
    const response = await axios.get (`${API}/GetAllData`);
    return parseList (response);
}

export const getDataByDates = async (startDate, endDate, summarize) => {
    let endpoint = `${API}/GetDataByDates?startDate=${startDate}&endDate=${endDate}&summarize=${summarize}`;
    const response = await axios.get (endpoint)
    return parseList (response);
}
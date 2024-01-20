import axios from "axios";
import API from './config';

export const getAllData = async () => {
    console.log(API)
    const response = await axios.get(`${API}/GetAllData`);
    console.log(response.data);
    return response;
}
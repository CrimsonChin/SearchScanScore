import axios from 'axios';

class API 
{
    static create(baseUrl){
        return axios.create({
            baseURL: baseUrl,
            responseType: "json"
        })
    }
}

export default API
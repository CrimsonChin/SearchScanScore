import API from './API'
import ConfigurationService from './ConfigurationService'

class GuardService {
    constructor(){
        this.api = API.create(ConfigurationService.FoundItService);
    }
    
    recordSighting(gameId, guardId, teamId){
         return this.api.post(`Guard/AddSighting/${gameId}/${guardId}/${teamId}`)
        .then(res => res.data)
    }
};

export default new GuardService();
import API from './API'
import ConfigurationService from './ConfigurationService'

class GuardService {
    constructor(){
        this.api = API.create(ConfigurationService.FoundItService);
    }
    
    recordSighting(gameId, teamId){
         return this.api.post(`Guard/RecordSighting/${gameId}/${teamId}`)
        .then(res => res.data)
    }
};

export default new GuardService();
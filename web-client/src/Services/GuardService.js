import API from './API'
import ConfigurationService from './ConfigurationService'

class GuardService {
    constructor(){
        this.api = API.create(ConfigurationService.FoundItService);
    }
    
    join(gameId, guardId){
        return this.api.get(`Guard/Join/${gameId}/${guardId}`)
        .then(res => res.data)
    }
    
    addSighting(gameId, guardId, teamId){
         return this.api.post(`Guard/Sighting/Add/${gameId}/${guardId}/${teamId}`)
        .then(res => res.data)
    }
};

export default new GuardService();
import API from './API'
import ConfigurationService from './ConfigurationService'

class TeamService {
    constructor(){
        this.api = API.create(ConfigurationService.FoundItService);
    }
    
    canJoinTeam(gameId, teamId){
        return this.api.get(`Team/CanJoinTeam/${gameId}/${teamId}`)
        .then(res => res.data)
    }

    collectItem(gameId, teamId, collectableItemId){
         return this.api.post(`Team/CollectItem/${gameId}/${teamId}/${collectableItemId}`)
        .then(res => res.data)
    }

    get(gameId, teamId){
        return this.api.get(`Team/Get/${gameId}/${teamId}`)
        .then(res => res.data)
    }
};

export default new TeamService();
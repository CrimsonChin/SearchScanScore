import API from './API'
import ConfigurationService from './ConfigurationService'

class TeamService {
    constructor(){
        this.api = API.create(ConfigurationService.FoundItService);
    }
    
    joinTeam(gameId, teamId){
        return this.api.get(`Team/Join/${gameId}/${teamId}`)
        .then(res => res.data)
    }

    addCollectedItem(gameId, teamId, collectableItemId){
        return this.api.post(`Team/CollectedItem/Add/${gameId}/${teamId}/${collectableItemId}`)
       .then(res => res.data)
   }

   getCollectableItems(gameId){
    return this.api.get(`Team/CollectableItem/Get/${gameId}`)
   .then(res => res.data)
}

    getCollectedItems(gameId, teamId){
         return this.api.get(`Team/CollectedItem/Get/${gameId}/${teamId}`)
        .then(res => res.data)
    }

    getSightings(gameId, teamId){
        return this.api.get(`Team/Sighting/Get/${gameId}/${teamId}`)
        .then(res => res.data)
    }

    // getCollectableItems(gameId, teamId){
    //     return this.api.get(`Team/CollectableItem/Get/${gameId}`)
    //     .then(res => res.data)
    // }

    getRemainingCollectableItems(gameId, teamId){
        return this.api.get(`Team/CollectableItem/GetRemaining/${gameId}/${teamId}`)
        .then(res => res.data)
    }
};

export default new TeamService();
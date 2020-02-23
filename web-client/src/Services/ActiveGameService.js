import API from './API'
import ConfigurationService from './ConfigurationService'

class ActiveGameService {
    constructor(){
        this.api = API.create(ConfigurationService.FoundItService);
    }
    
    joinGame(game, team){
        return this.api.get(`Game/Join/${game}/${team}`)
        .then(res => res.data)
    }

    collectItem(game, team, collectable){
        return this.api.post(`Game/CollectItem/${game}/${team}/${collectable}`)
        .then(res => res.data)
    }

    getCollectables(game){
        return this.api.get(`Game/GetCollectables/${game}`)
        .then(res => res.data)
    }
};

export default new ActiveGameService();
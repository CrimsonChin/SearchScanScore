import API from './API'
import ConfigurationService from './ConfigurationService'

class GameService {
    constructor(){
        this.api = API.create(ConfigurationService.FoundItService);
    }
    
    startGame(gameId){
        return this.api.put(`Admin/Game/Start/${gameId}`)
        .then(res => res.data)
    }

    stopGame(gameId){
        return this.api.put(`Admin/Game/Stop/${gameId}`)
        .then(res => res.data)
    }

    reset(gameId){
        return this.api.post(`Admin/Game/Reset/${gameId}`)
        .then(res => res.data)
    }

    get(gameId){
        return this.api.get(`Admin/Game/Get/${gameId}`)
        .then(res => res.data)
    }
};

export default new GameService();
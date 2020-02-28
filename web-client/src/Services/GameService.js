import API from './API'
import ConfigurationService from './ConfigurationService'

class GameService {
    constructor(){
        this.api = API.create(ConfigurationService.FoundItService);
    }
    
    startGame(gameId){
        return this.api.post(`Game/StartGame/${gameId}`)
        .then(res => res.data)
    }

    stopGame(gameId){
        return this.api.post(`Game/StopGame/${gameId}`)
        .then(res => res.data)
    }

    resetCollectedItems(gameId){
        return this.api.post(`Game/ResetCollectedItems/${gameId}`)
        .then(res => res.data)
    }

    reset(gameId){
        return this.api.post(`Game/Reset/${gameId}`)
        .then(res => res.data)
    }

    get(gameId){
        return this.api.get(`Game/Get/${gameId}`)
        .then(res => res.data)
    }
};

export default new GameService();
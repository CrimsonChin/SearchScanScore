import API from './API'
import ConfigurationService from './ConfigurationService'

class QrCodeService {
    constructor(){
        this.api = API.create(ConfigurationService.FoundItService);
    }
    
    getSource(externalId){
        return `${ConfigurationService.FoundItService}QrCode/Generate/${externalId}`
    }
};

export default new QrCodeService();

//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IClientDto  {
    id: string;
}

export class ClientDto  implements IClientDto{ 
    public id: string;

    constructor(id : string = null ) {
        
        this.id = id;
    }
}




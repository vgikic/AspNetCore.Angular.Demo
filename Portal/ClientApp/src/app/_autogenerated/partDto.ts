
//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IPartDto  {
    id: number;
    name: string;
}

export class PartDto  implements IPartDto{ 
    public id: number;
    public name: string;

    constructor(id : number = null,  name : string = null ) {
        
        this.id = id;
        this.name = name;
    }
}




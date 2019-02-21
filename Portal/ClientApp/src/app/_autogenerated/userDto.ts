
//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IUserDto  {
    id: number;
    userName: string;
}

export class UserDto  implements IUserDto{ 
    public id: number;
    public userName: string;

    constructor(id : number = null,  userName : string = null ) {
        
        this.id = id;
        this.userName = userName;
    }
}




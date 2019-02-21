
//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface ILoginBindingModel  {
    email: string;
    password: string;
}

export class LoginBindingModel  implements ILoginBindingModel{ 
    public email: string;
    public password: string;

    constructor(email : string = null,  password : string = null ) {
        
        this.email = email;
        this.password = password;
    }
}




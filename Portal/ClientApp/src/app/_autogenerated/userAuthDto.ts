import {ClaimDto} from './claimDto';

//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IUserAuthDto  {
    id: number;
    userName: string;
    bearerToken: string;
    isAuthenticated: boolean;
    claims: ClaimDto[];
    isInactive: boolean;
    isLocked: boolean;
    invalidCredentials: boolean;
}

export class UserAuthDto  implements IUserAuthDto{ 
    public id: number;
    public userName: string;
    public bearerToken: string;
    public isAuthenticated: boolean;
    public claims: ClaimDto[];
    public isInactive: boolean;
    public isLocked: boolean;
    public invalidCredentials: boolean;

    constructor(id : number = null,  userName : string = null,  bearerToken : string = null,  isAuthenticated : boolean = null,  claims : ClaimDto[] = null,  isInactive : boolean = null,  isLocked : boolean = null,  invalidCredentials : boolean = null ) {
        
        this.id = id;
        this.userName = userName;
        this.bearerToken = bearerToken;
        this.isAuthenticated = isAuthenticated;
        this.claims = claims;
        this.isInactive = isInactive;
        this.isLocked = isLocked;
        this.invalidCredentials = invalidCredentials;
    }
}




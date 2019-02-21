
//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IClaimDto  {
    id: number;
    roleId: number;
    claimType: string;
}

export class ClaimDto  implements IClaimDto{ 
    public id: number;
    public roleId: number;
    public claimType: string;

    constructor(id : number = null,  roleId : number = null,  claimType : string = null ) {
        
        this.id = id;
        this.roleId = roleId;
        this.claimType = claimType;
    }
}





//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IResponseDto  {
    message: string;
}

export class ResponseDto  implements IResponseDto{ 
    public message: string;

    constructor(message : string = null ) {
        
        this.message = message;
    }
}





//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IFileDto  {
    fullName: string;
    name: string;
    url: string;
    base64: string;
}

export class FileDto  implements IFileDto{ 
    public fullName: string;
    public name: string;
    public url: string;
    public base64: string;

    constructor(fullName : string = null,  name : string = null,  url : string = null,  base64 : string = null ) {
        
        this.fullName = fullName;
        this.name = name;
        this.url = url;
        this.base64 = base64;
    }
}




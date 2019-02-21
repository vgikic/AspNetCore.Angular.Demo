
//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IFileBindingModel  {
    bytes: number[];
    name: string;
}

export class FileBindingModel  implements IFileBindingModel{ 
    public bytes: number[];
    public name: string;

    constructor(bytes : number[] = null,  name : string = null ) {
        
        this.bytes = bytes;
        this.name = name;
    }
}




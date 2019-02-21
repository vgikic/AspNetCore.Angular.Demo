
//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface IItemBindingModel  {
    name: string;
    email: string;
    from: Date;
    to: Date;
    isActive: boolean;
    optionId: number;
    categoryId: number;
    parts: number[];
}

export class ItemBindingModel  implements IItemBindingModel{ 
    public name: string;
    public email: string;
    public from: Date;
    public to: Date;
    public isActive: boolean;
    public optionId: number;
    public categoryId: number;
    public parts: number[];

    constructor(name : string = null,  email : string = null,  from : Date = null,  to : Date = null,  isActive : boolean = null,  optionId : number = null,  categoryId : number = null,  parts : number[] = null ) {
        
        this.name = name;
        this.email = email;
        this.from = from;
        this.to = to;
        this.isActive = isActive;
        this.optionId = optionId;
        this.categoryId = categoryId;
        this.parts = parts;
    }
}




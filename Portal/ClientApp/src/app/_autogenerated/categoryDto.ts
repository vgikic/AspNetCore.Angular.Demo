
//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface ICategoryDto  {
    id: number;
    name: string;
}

export class CategoryDto  implements ICategoryDto{ 
    public id: number;
    public name: string;

    constructor(id : number = null,  name : string = null ) {
        
        this.id = id;
        this.name = name;
    }
}




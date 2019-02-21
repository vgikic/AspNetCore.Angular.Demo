import { addDays } from "../helpers/forms/date-helper";
import { ItemBindingModel } from "../_autogenerated/itemBindingModel";

export class ItemBindingModelExtended extends ItemBindingModel {
    public id = 0;
    constructor() {
        super();
        this.isActive = false;
        this.optionId = 0;
        this.from = new Date();
        this.to = addDays(new Date(), 7);
    }

}
import { FormGroup } from "@angular/forms";
import { IJsonPatchObject } from "../../models/interfaces";

export class PatchHelper {
  public static generatePatchObjects(form: FormGroup, obj?: Object): Array<IJsonPatchObject> {
    const patchObj: Array<IJsonPatchObject> = [];
    for (let prop in form.controls) {
      if (form.controls[prop].dirty && (!obj || obj.hasOwnProperty(prop))) {
        patchObj.push({
          op: 'replace',
          path: `/${prop}`,
          value: form.controls[prop].value
        })
      }
    }
    return patchObj;
  }

  public static generatePatchObjectForArrayValue(path: string, arr: Array<number>): IJsonPatchObject {
    return {
      op: 'replace',
      path: path,
      value: arr
    };
  }
}

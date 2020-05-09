import { LocalizationService } from "abp-ng2-module/dist/src/localization/localization.service";
import { AppConsts } from "@shared/AppConsts";

export class AzmoonLocalizationService extends LocalizationService {
    localizationSourceName = AppConsts.localization.defaultLocalizationSourceName;
    constructor() {
        super();
    }

    localize(key: string, sourceName: string): string {
        let found = false;
        var translated = abp.localization.localize(key, this.localizationSourceName);
        if (key != translated) {
            // translation found
            return translated;
        }
        if (abp.localization.values[this.localizationSourceName][key]) {
            found = true;
        }
        if (!found)
            {
                console.log('localization entry not found for key: ' + key)
                return `[${key}]`;
            }
        return translated;
    }

}
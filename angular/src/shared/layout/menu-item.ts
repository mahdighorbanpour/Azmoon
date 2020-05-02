export class MenuItem {
    name = '';
    permissionName = '';
    icon = '';
    route = '';
    cssClass: string;
    items: MenuItem[];

    constructor(name: string, permissionName: string, icon: string, route: string, cssClass:string = '', childItems: MenuItem[] = null) {
        this.name = name;
        this.permissionName = permissionName;
        this.icon = icon;
        this.route = route;
        this.cssClass = cssClass;
        if (childItems) {
            this.items = childItems;
        } else {
            this.items = [];
        }
    }
}

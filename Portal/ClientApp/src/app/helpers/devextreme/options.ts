import DevExpress from "devextreme/bundles/dx.all";
import { createStore } from "devextreme-aspnet-data-nojquery";

export function createDxDataGridOptions(options: DevExpress.ui.dxDataGridOptions, loadUrl: string, insUrl?: string, updUrl?: string, delUrl?: string) {
    const gridOptions = Object.assign(dxDataGridOptions, options);
    const store = createStore({
        key: "id",
        loadUrl: loadUrl,
        insertUrl: insUrl,
        updateUrl: updUrl,
        deleteUrl: delUrl,
    });
    gridOptions.dataSource = store as any;
    return gridOptions;
}

export const dxDataGridOptions: DevExpress.ui.dxDataGridOptions = {
    remoteOperations: true,
    rowAlternationEnabled: true,
    hoverStateEnabled: true,
    allowColumnResizing: true,
    columnAutoWidth: false,
    paging: {
        enabled: true,
        pageSize: 7
    },
    export: {
        enabled: true,
        fileName: "export-example",
    },
    selection: {
        mode: 'single',
        allowSelectAll: false
    },
    filterRow: {
        visible: true,
        applyFilter: 'auto'
    },
    searchPanel: {
        visible: true,
    },
    headerFilter: {
        visible: true
    },

};
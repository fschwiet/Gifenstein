
function RowTypeManager() {
    this._rowTypes = {};
    this._rowList = [];
}

RowTypeManager.prototype.addRowType = function (name) {
    this._rowTypes[name] = {
        constructor: window[name],
        index: this._rowList.length
    };
    this._rowList.push(name);
};

RowTypeManager.prototype.BuildRow = function (name) {
    var constructor = this._rowTypes[name].constructor;
    return new constructor();
};

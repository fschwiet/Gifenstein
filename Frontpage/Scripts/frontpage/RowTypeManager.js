
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

RowTypeManager.prototype.GetAdjacent = function(name) {
    var index = this._rowTypes[name].index;
    var typeCount = this._rowList.length;
    return [this._rowList[(index - 1 + typeCount) % typeCount], this._rowList[(index + 1 + typeCount) % typeCount]];
};

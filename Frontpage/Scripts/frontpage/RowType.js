
function ImageRow() {
}

ImageRow.prototype.Render = function () {
    var result = $("<li><img></li>");
    result.find("img").attr("src", this.imageUrl);
    return result;
};

ImageRow.prototype.StartEdit = function() {
};

function AllRightAnnouncement() {
    this.imageUrl = "/Content/AllRightGentlemen_dialog.png";
    this.announcement = "We're going to master the gif technology.";
}

$.extend(AllRightAnnouncement.prototype, ImageRow.prototype);

AllRightAnnouncement.prototype.StartEdit = function () {
    
    var templateSource = "<fieldset><label>What text should be written for this frame?</label> <input class=\"input-block-level\" type=\"text\" placeholder=\"Type something..\" value=\"{{announcement}}\"/><button>Submit</button></fieldset>";
    var template = Handlebars.compile(templateSource);

    var content = $(template(this));
    content.dialog({
        modal: true,
        width: 480,
    });

    var that = this;
    content.find("button").click(function() {
        that.announcement = $("input[type=text]").val();
        content.dialog("close");
        content.remove();
    });
    
    $("body").append();
}

function AllRightAnimationFrame() {
}

$.extend(AllRightAnimationFrame.prototype, ImageRow.prototype);

AllRightAnimationFrame.prototype.StartEdit = function () {
    
    var templateSource = "<fieldset><label>Please pick an url for an animated gif for this frame.</label> <input class=\"input-block-level\" type=\"text\" placeholder=\"Url here..\" value=\"{{customUrl}}\"/><button>Submit</button></fieldset>";
    var template = Handlebars.compile(templateSource);

    var content = $(template(this));
    content.dialog({
        modal: true,
        width: 480,
    });

    var that = this;
    content.find("button").click(function () {
        that.customUrl = $("input[type=text]").val();
        content.dialog("close");
        content.remove();
    });

    $("body").append();
}

function AllRightMinor() {
    this.imageUrl = "/Content/AllRightGentlemen_unimpressed.png";
    this.customUrl = "http://i2.kym-cdn.com/photos/images/masonry/000/306/930/d20.gif";
}

$.extend(AllRightMinor.prototype, AllRightAnimationFrame.prototype);

function AllRightMajor() {
    this.imageUrl = "/Content/AllRightGentlemen_impressed.png";
    this.customUrl = "http://i1.kym-cdn.com/photos/images/masonry/000/422/365/4ef.gif";
}

$.extend(AllRightMajor.prototype, AllRightAnimationFrame.prototype);

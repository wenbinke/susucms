/* Modified from jquery.jqtransform
----------------------------------------------*/
(function ($) {

    /*  Labels
    ---------------------------------------*/
    var transFormGetLabel = function (objfield) {
        var selfForm = $(objfield.get(0).form);
        var oLabel = objfield.next();
        if (!oLabel.is('label')) {
            oLabel = objfield.prev();
            if (oLabel.is('label')) {
                var inputname = objfield.attr('id');
                if (inputname) {
                    oLabel = selfForm.find('label[for="' + inputname + '"]');
                }
            }
        }
        if (oLabel.is('label')) { return oLabel.css('cursor', 'pointer'); }
        return false;
    };

    /* Hide all open selects */
    var transFormHideSelect = function (oTarget) {
        var ulVisible = $('.s-form-select-wrapper ul:visible');
        ulVisible.each(function () {
            var oSelect = $(this).parents(".s-form-select-wrapper:first").find("select").get(0);
            //do not hide if click on the label object associated to the select
            if (!(oTarget && oSelect.oLabel && oSelect.oLabel.get(0) == oTarget.get(0))) { $(this).hide(); }
        });
    };
    /* Check for an external click */
    var transFormCheckExternalClick = function (event) {
        if ($(event.target).parents('.s-form-select-wrapper').length === 0) { transFormHideSelect($(event.target)); }
    };

    /* Apply document listener */
    var transFormAddDocumentListener = function () {
        $(document).mousedown(transFormCheckExternalClick);
    };

    /* Add a new handler for the reset action */
    var transFormReset = function (f) {
        var sel;
        $('.s-form-select-wrapper select', f).each(function () {
            sel = (this.selectedIndex < 0) ? 0 : this.selectedIndex; $('ul', $(this).parent()).each(function () { $('a:eq(' + sel + ')', this).click(); });
        });
        $('a.s-form-checkbox, a.s-form-radio', f).removeClass('s-form-checked');
        $('input:checkbox, input:radio', f).each(function () { if (this.checked) { $('a', $(this).parent()).addClass('s-form-checked'); } });
    };

    /* Text Fields 
    --------------------------------------------------------*/
    $.fn.transInputText = function () {
        return this.each(function () {
            var $input = $(this);

            if ($input.hasClass('s-form-done') || $input.closest('.t-editor').length > 0) { return; }
            $input.addClass('s-form-done');

            var oLabel = transFormGetLabel($(this));
            oLabel && oLabel.bind('click', function () { $input.focus(); });

            var inputSize = $input.width();
            $input.css('width', inputSize - 16);

            $input.wrap('<div class="s-form-input-wrapper"></div>');

            var $wrapper = $input.parent('.s-form-input-wrapper');
            $input.focus(function () {
                $wrapper.addClass("s-form-input-wrapper-focus");
            }).blur(function () {
                $wrapper.removeClass("s-form-input-wrapper-focus");
            }).hover(function () {
                $wrapper.addClass("s-form-input-wrapper-hover");
            }, function () {
                $wrapper.removeClass("s-form-input-wrapper-hover");
            });

            this.wrapper = $wrapper;
        });
    };

    /* Check Boxes 
    ---------------------------------------*/
    $.fn.transCheckBox = function () {
        return this.each(function () {
            if ($(this).hasClass('s-form-hide')) { return; }

            var $input = $(this);

            var aLink = $('<a href="#" class="s-form-checkbox"></a>');

            $input.addClass('s-form-hide').wrap('<span class="s-form-checkbox-wrapper"></span>').parent().prepend(aLink);

            //on change, change the class of the link
            $input.change(function () {
                this.checked && aLink.addClass('s-form-checked') || aLink.removeClass('s-form-checked');
                return true;
            });

            $input.data('sCheckBox', {
                val: function (value) {
                    if (value == true || value == 'true') {
                        $input.attr('checked', 'checked');
                        aLink.addClass('s-form-checked')
                    } else {
                        $input.removeAttr('checked');
                        aLink.removeClass('s-form-checked');
                    }
                }
            });

            // Click Handler, trigger the click and change event on the input
            aLink.click(function () {
                if ($input.attr('disabled')) {
                    return false;
                }

                $input.click();
                if ($.browser.msie) {
                    $input.change();
                }

                return false;
            });

            //set the click on the label
            var oLabel = transFormGetLabel($input);
            oLabel && oLabel.click(function () { aLink.click(); });

            // set the default state
            this.checked && aLink.addClass('s-form-checked');
        });
    };

    /*  Radio Buttons 
    -----------------------------------------------------*/
    $.fn.transRadio = function () {
        return this.each(function () {
            if ($(this).hasClass('s-form-hide')) { return; }

            var $input = $(this);
            var inputSelf = this;

            oLabel = transFormGetLabel($input);
            oLabel && oLabel.click(function () { aLink.trigger('click'); });

            var aLink = $('<a href="#" class="s-form-radio" rel="' + this.name + '"></a>');
            $input.addClass('s-form-hide').wrap('<span class="s-form-radio-wrapper"></span>').parent().prepend(aLink);

            $input.change(function () {
                inputSelf.checked && aLink.addClass('s-form-checked') || aLink.removeClass('s-form-checked');
                return true;
            });

            // Click Handler
            aLink.click(function () {
                if ($input.attr('disabled')) { return false; }
                $input.trigger('click').trigger('change');

                // uncheck all others of same name input radio elements
                $('input[name="' + $input.attr('name') + '"]', inputSelf.form).not($input).each(function () {
                    $(this).attr('type') == 'radio' && $(this).trigger('change');
                });

                return false;
            });
            // set the default state
            inputSelf.checked && aLink.addClass('s-form-checked');
        });
    };

    /* TextArea 
    ------------------------------------------------*/
    $.fn.transTextarea = function () {
        return this.each(function () {
            var textarea = $(this);

            if (textarea.hasClass('s-form-done') || textarea.closest('.t-editor').length > 0 || textarea.closest('.code-editor').length > 0) { return; }
            textarea.addClass('s-form-done');

            oLabel = transFormGetLabel(textarea);
            oLabel && oLabel.click(function () { textarea.focus(); });

            var strTable = '<table cellspacing="0" cellpadding="0" border="0" class="s-form-textarea">';
            strTable += '<tr><td id="s-form-textarea-tl"></td><td id="s-form-textarea-tm"></td><td id="s-form-textarea-tr"></td></tr>';
            strTable += '<tr><td id="s-form-textarea-ml">&nbsp;</td><td id="s-form-textarea-mm"><div></div></td><td id="s-form-textarea-mr">&nbsp;</td></tr>';
            strTable += '<tr><td id="s-form-textarea-bl"></td><td id="s-form-textarea-bm"></td><td id="s-form-textarea-br"></td></tr>';
            strTable += '</table>';
            var oTable = $(strTable)
					.insertAfter(textarea)
					.hover(function () {
					    !oTable.hasClass('s-form-textarea-focus') && oTable.addClass('s-form-textarea-hover');
					}, function () {
					    oTable.removeClass('s-form-textarea-hover');
					})
				;

            textarea
				.focus(function () { oTable.removeClass('s-form-textarea-hover').addClass('s-form-textarea-focus'); })
				.blur(function () { oTable.removeClass('s-form-textarea-focus'); })
				.appendTo($('#s-form-textarea-mm div', oTable))
			;
            this.oTable = oTable;
        });
    };

    /* Select 
    -------------------------------------*/
    $.fn.transSelect = function () {
        return this.each(function (index) {
            var $select = $(this);

            if ($select.hasClass('s-form-hide') || $select.attr('multiple')) { return; }

            var oLabel = transFormGetLabel($select);

            /* First thing we do is Wrap it */
            var $wrapper = $select
				.addClass('s-form-hide')
				.wrap('<div class="s-form-select-wrapper"></div>')
				.parent()
				.css({ zIndex: 10 - index });

            /* Now add the html for the select */
            $wrapper.prepend('<div><span></span><a href="#"></a></div><ul></ul>');

            var $ul = $('ul', $wrapper);

            /* Now we add the options */
            $('option', this).each(function () {
                var oLi = $('<li><a href="#">' + $(this).text() + '</a></li>');
                $ul.append(oLi);
            });

            /* Add click handler to the a */
            $ul.find('a').click(function () {
                $wrapper.find('ul li a.selected').removeClass('selected');

                $(this).addClass('selected');

                var index = $(this).parent('li').index();
                console.log(index);

                /* Fire the onchange event */
                if ($select[0].selectedIndex != index) {
                    $select[0].selectedIndex = index;
                    $select.change();
                }
                $select[0].selectedIndex = index;
                $('span:eq(0)', $wrapper).html($(this).html());

                $ul.hide();

                return false;
            });

            function setValue(selectedIndex) {
                $wrapper.find('> div span').html($select.find('option').eq(selectedIndex).text());
                $wrapper.find('ul li a.selected').removeClass('selected');
                $wrapper.find('ul li a').eq(selectedIndex).addClass('selected');
            }

            /* Set the default */
            setValue(this.selectedIndex);

            oLabel && oLabel.click(function () {
                $wrapper.find('> div a').click();
            });
            this.oLabel = oLabel;

            /* Apply the click handler to the Open */
            var oLinkOpen = $wrapper.find('> div')
				.click(function () {

				    //Check if box is already open to still allow toggle, but close all other selects
				    if ($ul.is(':hidden')) {
				        transFormHideSelect();
				    }

				    if ($select.attr('disabled')) { return false; }

				    $ul.slideToggle('fast', function () {
				        var offSet = ($('a.selected', $ul).offset().top - $ul.offset().top);
				        $ul.animate({ scrollTop: offSet });
				    });

				    return false;
				});

            // Set the new width
            var iSelectWidth = $select.outerWidth();
            var newWidth = iSelectWidth + oLinkOpen.outerWidth();
            $wrapper.css({ width: newWidth });
            $ul.css({ width: newWidth - 2 });
        });
    };

    $.fn.transForm = function () {
        return this.each(function () {
            var selfForm = $(this);
            if (selfForm.hasClass('s-form-done')) { return; }
            selfForm.addClass('s-form-done');

            $('input:text, input:password', this).transInputText();
            $('input:checkbox', this).transCheckBox();
            $('input:radio', this).transRadio();
            $('textarea', this).transTextarea();

            if ($('select', this).transSelect().length > 0) { transFormAddDocumentListener(); }
            selfForm.bind('reset', function () { var action = function () { transFormReset(this); }; window.setTimeout(action, 10); });
        });
    };
})(jQuery);
				   
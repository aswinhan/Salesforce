document.addEventListener("DOMContentLoaded", function () {
    const addRowBtn = $("#addRowBtn");
    const removeRowBtn = $("#removeRowBtn");
    const rowsContainer = $("#rowsContainer");
    const primarySourceDiv = $("#primarySourceVerificationDetails");

    function createRow(labelText, checkboxId) {
        const row = $(`
            <div class="row" data-checkbox-id="${checkboxId}">
                <div class="col-sm-3">
                    <div class="mb-3">
                        <label class="form-label">Title</label>
                        <input type="text" class="form-control" placeholder="Title" name="title">
                    </div>
                </div><!-- Col -->
                <div class="col-sm-3">
                    <div class="mb-3">
                        <label class="form-label">Path On Client</label>
                        <input class="form-control" type="file" id="formFile" name="pathOnClient">
                    </div>
                </div><!-- Col -->
                <div class="col-sm-3">
                    <div class="mb-3">
                        <label class="form-label">Version Data</label>
                        <input type="text" class="form-control" placeholder="Version Data" name="versionData">
                    </div>
                </div><!-- Col -->
                <div class="col-sm-3">
                    <div class="mb-3">
                        <label class="form-label">UC Document Type</label>
                        <input type="text" class="form-control" placeholder="UC Document Type" name="ucDocumentType" value="${labelText}">
                    </div>
                </div><!-- Col -->
            </div>
        `);
        return row;
    }

    function addNewRow(labelText, checkboxId) {
        rowsContainer.append(createRow(labelText, checkboxId));
    }

    function updateRows() {
        if (!primarySourceDiv.length) {
            addRowBtn.prop("disabled", false);
            return;
        }

        const checkboxes = primarySourceDiv.find("input[type='checkbox']:not(.toggle-checkbox)");
        const checkedEnabledCount = checkboxes.filter(":checked:not(:disabled)").length;
        const totalCheckedCount = checkboxes.filter(":checked").length;
        const uncheckedEnabledCount = checkboxes.not(":checked,:disabled").length;

        const currentRows = rowsContainer.children().length;
        const existingCheckboxIds = rowsContainer.children().map(function () {
            return $(this).data('checkbox-id');
        }).get();

        // Add rows for newly checked checkboxes
        checkboxes.each(function () {
            const checkboxId = $(this).attr('id');
            if ($(this).is(":checked") && !$(this).is(":disabled") && !existingCheckboxIds.includes(checkboxId)) {
                const labelText = $(this).nextAll().first().contents().filter(function () {
                    return this.nodeType === 3; // Node.TEXT_NODE
                }).text().trim();
                addNewRow(labelText, checkboxId);
            }
        });

        // Remove rows for unchecked or disabled checkboxes
        checkboxes.each(function () {
            const checkboxId = $(this).attr('id');
            if ((!$(this).is(":checked") || $(this).is(":disabled")) && existingCheckboxIds.includes(checkboxId)) {
                rowsContainer.children(`[data-checkbox-id='${checkboxId}']`).remove();
            }
        });

        addRowBtn.prop("disabled", (checkedEnabledCount === 0 || totalCheckedCount === checkboxes.length || checkedEnabledCount === currentRows || uncheckedEnabledCount > 0));
    }

    function monitorCheckboxes() {
        const checkboxes = primarySourceDiv.find("input[type='checkbox']:not(.toggle-checkbox)");
        checkboxes.each(function () {
            $(this).on('change', updateRows);
            const observer = new MutationObserver(updateRows);
            observer.observe(this, { attributes: true, attributeFilter: ['disabled', 'checked'] });
        });
    }

    if (primarySourceDiv.length) {
        monitorCheckboxes();
        updateRows();

        addRowBtn.on("click", function () {
            if (!addRowBtn.prop("disabled")) {
                updateRows();
            }
        });

        removeRowBtn.on("click", function () {
            if (rowsContainer.children().last().length) {
                rowsContainer.children().last().remove();
            }
        });
    } else {
        addNewRow("UC Document Type", "default");

        addRowBtn.on("click", function () {
            addNewRow("UC Document Type", "default");
        });

        removeRowBtn.on("click", function () {
            if (rowsContainer.children().last().length) {
                rowsContainer.children().last().remove();
            }
        });
    }

    // Handle form submission
    const GenerateJsonButton = $("#GenerateJson");

    GenerateJsonButton.on("click", function () {
        // Collect form data
        const formData = collectFormData();
        console.log(formData);
    });

    function collectFormData() {
        const formInputs = rowsContainer.find(".row input");
        const formData = [];

        formInputs.each(function () {
            const fieldName = $(this).attr("name");
            const fieldValue = $(this).val();
            formData.push({ [fieldName]: fieldValue });
        });

        return formData;
    }
});

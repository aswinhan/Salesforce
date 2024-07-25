document.addEventListener("DOMContentLoaded", function () {
    const addRowBtn = document.getElementById("addRowBtn");
    const removeRowBtn = document.getElementById("removeRowBtn");
    const rowsContainer = document.getElementById("rowsContainer");
    const primarySourceDiv = document.getElementById("primarySourceVerificationDetails");

    function createRow() {
        const row = document.createElement("div");
        row.className = "row";
        row.innerHTML = `
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
                    <input type="text" class="form-control" placeholder="UC Document Type" name="ucDocumentType">
                </div>
            </div><!-- Col -->
        `;
        return row;
    }

    function addNewRow() {
        rowsContainer.appendChild(createRow());
    }

    function updateRows() {
        if (!primarySourceDiv) {
            addRowBtn.disabled = false;
            return;
        }

        const checkboxes = primarySourceDiv.querySelectorAll("input[type='checkbox']:not(.toggle-checkbox)");
        const checkedEnabledCount = Array.from(checkboxes).filter(cb => cb.checked && !cb.disabled).length;
        const totalCheckedCount = Array.from(checkboxes).filter(cb => cb.checked).length;
        const unchekedEnabledCount = Array.from(checkboxes).filter(cb => !cb.checked && !cb.disabled).length;

        const currentRows = rowsContainer.children.length;
        if (checkedEnabledCount > currentRows) {
            for (let i = currentRows; i < checkedEnabledCount; i++) {
                addNewRow();
            }
        } else if (checkedEnabledCount < currentRows) {
            for (let i = currentRows; i > checkedEnabledCount; i--) {
                if (rowsContainer.lastElementChild) {
                    rowsContainer.removeChild(rowsContainer.lastElementChild);
                }
            }
        }

        addRowBtn.disabled = (checkedEnabledCount === 0 || totalCheckedCount === checkboxes.length || checkedEnabledCount === currentRows || unchekedEnabledCount > 0);
    }

    function monitorCheckboxes() {
        const checkboxes = primarySourceDiv.querySelectorAll("input[type='checkbox']:not(.toggle-checkbox)");
        checkboxes.forEach(checkbox => {
            checkbox.addEventListener('change', updateRows);
            const observer = new MutationObserver(updateRows);
            observer.observe(checkbox, { attributes: true, attributeFilter: ['disabled', 'checked'] });
        });
    }

    if (primarySourceDiv) {
        monitorCheckboxes();
        updateRows();

        addRowBtn.addEventListener("click", function () {
            if (!addRowBtn.disabled) {
                updateRows();
            }
        });

        removeRowBtn.addEventListener("click", function () {
            if (rowsContainer.lastElementChild) {
                rowsContainer.removeChild(rowsContainer.lastElementChild);
            }
        });
    } else {
        addNewRow();

        addRowBtn.addEventListener("click", addNewRow);

        removeRowBtn.addEventListener("click", function () {
            if (rowsContainer.lastElementChild) {
                rowsContainer.removeChild(rowsContainer.lastElementChild);
            }
        });
    }

    // Handle form submission
    const GenerateJsonButton = document.getElementById("GenerateJson");

    GenerateJsonButton.addEventListener("click", function () {
        // Collect form data
        const formData = collectFormData();
        console.log(formData);
    });
    function collectFormData() {
        const formInputs = rowsContainer.querySelectorAll(".row input");
        const formData = [];

        formInputs.forEach(input => {
            const fieldName = input.getAttribute("name");
            const fieldValue = input.value;
            formData.push({ [fieldName]: fieldValue });
        });

        return formData;
    }
});
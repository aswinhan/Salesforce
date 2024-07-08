document.addEventListener("DOMContentLoaded", function () {
    const addRowBtn = document.getElementById("addRowBtn");
    const removeRowBtn = document.getElementById("removeRowBtn");
    const rowsContainer = document.getElementById("rowsContainer");

    function addNewRow() {
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
        rowsContainer.appendChild(row);
    }

    // Call addNewRow function on page load
    addNewRow();

    addRowBtn.addEventListener("click", function () {
        addNewRow();
    });

    removeRowBtn.addEventListener("click", function () {
        if (rowsContainer.lastElementChild) {
            rowsContainer.removeChild(rowsContainer.lastElementChild);
        }
    });

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


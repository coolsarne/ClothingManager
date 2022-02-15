const urlParams = new URLSearchParams(window.location.search);
const clothingPieceId = parseInt(urlParams.get("clothingPieceId"));
const saveButton = document.getElementById('addDesigner');

saveButton.addEventListener('click',() => addDesignerToClothingPiece())

loadDesigners();
loadAvailableDesigners();

function loadDesigners() {
    fetch(`/api/clothingPieces/${clothingPieceId}/designers`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => {
            if (response.ok) return response.json()
            else alert("Something went wrong");
        })
        .then(data => showDesigners(data))
        .catch(reason => alert("Call failed: " + reason));
}

function showDesigners(designers){
    const table = document.getElementById('designerTableHead')
    table.innerHTML =
        `<tr>
                <th>Name</th>
                <th>Age</th>
                <th>Nationality</th>
            </tr>`;
    document.getElementById('designerTableBody').innerHTML = "";
    designers.forEach(designer => addDesigner(designer));
}

function addDesigner(designer){
    const table = document.getElementById('designerTableBody')
    table.innerHTML += `
        <tr>
            <td>${designer.name}</td>
            <td>${designer.age}</td>
            <td>${designer.nationality}</td>
        </tr>
    `
}

function loadAvailableDesigners() {
    fetch(`/api/clothingPieceDesigners/${clothingPieceId}/availableDesigners`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showSelectBox(data))
        .catch(reason => alert("Call failed: " + reason));
}

function showSelectBox(designers){
    const select = document.getElementById('designer');
    select.innerHTML = "";
    designers.forEach(d => {
        select.innerHTML += `
            <option value=${d.id}>${d.name}</option>
        `
    })
}

function addDesignerToClothingPiece() {
    loadDesigners();
    loadAvailableDesigners();
    const designerId = document.getElementById('designer').value;
    const contributionOrder = document.getElementById('contribution').value;
    const clothingPieceDesignerDTO = {clothingpieceid: clothingPieceId, designerid: designerId, contributionorder: contributionOrder};

    fetch(`/api/clothingPieceDesigners`, {
        method: "POST",
        body: JSON.stringify(clothingPieceDesignerDTO),
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(function (response) {
            if (response.ok) {
                loadDesigners();
                loadAvailableDesigners();
            } else {
                alert("Invalid input detected");
            }
        })
        .catch(function (error) {

        });
    
    

}

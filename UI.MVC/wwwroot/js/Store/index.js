const loadResponseButton = document.getElementById('loadStores');
loadResponseButton.addEventListener('click', () => loadStores());

loadStores();

function loadStores() {
    fetch('/api/stores', {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showStores(data))
        .catch(reason => alert("Call failed: " + reason));
}

function showStores(stores) {
    const table = document.getElementById('storeTableHead')
    table.innerHTML = 
        `<tr>
                <th>City</th>
                <th>Zipcode</th>
                <th>Name</th>
                <th>Details</th>
            </tr>`;
    document.getElementById('storeTableBody').innerHTML = "";
    stores.forEach(store => addStore(store));
}

function addStore(store){
    const table = document.getElementById('storeTableBody')
    table.innerHTML += `
        <tr>
            <td>${store.city}</td>
            <td>${store.zipcode}</td>
            <td>${store.name}</td>
            <td><a href="/Store/Details?storeId=${store.id}">Details</a></td>
        </tr>
    `
}
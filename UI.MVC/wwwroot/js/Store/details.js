const urlParams = new URLSearchParams(window.location.search);
const storeId = parseInt(urlParams.get("storeId"));
const loadResponseButton = document.getElementById('save');
loadResponseButton.addEventListener("click", () => saveChanges())

loadStore();

function loadStore() {
    fetch(`/api/stores/${storeId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => {
            if (response.ok) return response.json()
            else alert("Something went wrong");
        })
        .then(data => showStore(data))
        .catch(reason => alert("Call failed: " + reason));
}

function showStore(store) {
    document.getElementById("city").value = store.city;
    document.getElementById("zipcode").value = store.zipcode;
    document.getElementById("name").value = store.name;
}

function saveChanges() {
    const newCity = document.getElementById("city").value;
    const newZipcode = document.getElementById("zipcode").value;
    const newName = document.getElementById("name").value;
    const updatedStoreDTO = {city: newCity, zipcode: newZipcode, name: newName, id: storeId};
    fetch(`/api/stores`, {
        method: "PUT",
        body: JSON.stringify(updatedStoreDTO),
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(function (response) {
            if (response.ok) {
                window.location.href = "/Store"
            } else {
                alert("Invalid input detected");
            }
        })
        .catch(function (error) {

        });
}

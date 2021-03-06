const urlParams = new URLSearchParams(window.location.search);
const storeId = parseInt(urlParams.get("storeId"));
const loadResponseButton = document.getElementById('save');
loadResponseButton.addEventListener("click", () => saveChanges());

loadStore();

function loadStore() {
    fetch(`/api/stores/${storeId}`, {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        })
        .then(response => {
            if (response.ok) return response.json();
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

const storeCity = document.getElementById("city").value;
const storeName = document.getElementById("name").value;
const storeZipCode = document.getElementById("zipcode").value;

function saveChanges() {
    var newCity = document.getElementById("city").value;
    var newZipcode = document.getElementById("zipcode").value;
    var newName = document.getElementById("name").value;

    if (newCity === "") {
        newCity = null;
    }
    if (newZipcode === "") {
        newZipcode = null;
    }
    if (newName === "") {
        newName = null;
    }
    const updatedStoreDTO = { city: newCity, zipcode: newZipcode, name: newName, id: storeId };

    fetch(`/api/stores`, {
            method: "PATCH",
            body: JSON.stringify(updatedStoreDTO),
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(function (response) {
            if (response.ok) {
                window.location.href = "/Store";
            } else {
                alert("Invalid input detected");
            }
        })
        .catch(function (error) {

        });
}
class CartHatItem {
  constructor({id, name, size, length, width, depth, price, quantity, comment, imageUrl, hatTypeId, materials = []}) {
      this.id = id;
      this.name = name;
      this.size = size;
      this.length = length;
      this.width = width;
      this.depth = depth;
      this.price = price;
      this.quantity = quantity;
      this.comment = comment;
      this.imageUrl = imageUrl,
      this.hatTypeId = hatTypeId,
      this.materials = materials.map(m => new HatMaterial(m.materialId, m.quantity, m.name, m.price, m.unit));
  }

  static async fromApi(id) {
    const [details, materials] = await Promise.all([
      $.get('/hats/details', { id }),
      $.get('/materials/hatMaterials', { hatId: id })
    ]);

    const item = new CartHatItem({
      id: id,
      name: details.name,
      size: details.size,
      length: details.length,
      width: details.width,
      depth: details.depth,
      price: details.price,
      quantity: 1,
      comment: details.comment,
      imageUrl: details.imageUrl,
      hatTypeId: details.hatTypeId,
    });

    item.materials = materials.map(m => new HatMaterial(m.materialId, m.quantity, m.name, m.price, m.unit));
    return item;
  }

  addMaterial(hatMaterial){
    this.materials.push(hatMaterial)
  }

  removeMaterial(materialId){
    this.materials = this.materials.filter(x => x.materialId != materialId);
  }
} 

class HatMaterial {
  constructor(materialId, quantity, name, price, unit){
    this.materialId = materialId,
    this.quantity = quantity
    this.name = name,
    this.price = price
    this.unit = unit
  }

  changeQuantity(quantity){
    this.quantity = quantity;
  }
}

class Cart {
  constructor() {
      this.items = []
  }

  async addItem(itemId) {
    let item = this.items.find(x => x.id == itemId);
    if (item) {
      item.quantity += 1;
    } else {
      item = await CartHatItem.fromApi(itemId);
      this.items.push(item)
    }
    this.syncToStorage();
  }

  removeItem(itemId) {
    let item = this.items.find(x => x.id == itemId);
    if (item) {
      if (item.quantity > 1) item.quantity -= 1;
      else{
        this.items = this.items.filter(x => x.id !== itemId);
      }
    }
    this.syncToStorage();
  }

  clearCart(){
    this.items = [];
    this.syncToStorage();
  }

  getItem(id) {
    return this.items.find(x => x.id == id);
  }

  syncToStorage() {
    const plain = this.items.map(i => ({ ...i }));
    localStorage.setItem('cart', JSON.stringify(plain));
  }

  loadFromStorage() {
    const saved = JSON.parse(localStorage.getItem('cart') || '{}');
    console.log( Object.values(saved).map(data => new CartHatItem(data)))
    if (typeof saved === 'object' && saved !== null) {
      this.items = Object.values(saved).map(data => new CartHatItem(data));
    } else {
      this.items = [];
    }
  }

  total() {
    return this.items.reduce((sum, item) => sum + (item.price * item.quantity), 0);
  }
}

const cart = new Cart();
let availableMaterials = [];
let selectedMaterials = [];

async function loadAvailableMaterials() {
  try {
    const response = await fetch('/Materials/allMaterials');
    if (!response.ok) {
      throw new Error('Failed to fetch materials');
    }
    const data = await response.json();
    availableMaterials = data; // Save the array of materials here
    console.log('Loaded materials:', availableMaterials);
  } catch (error) {
    console.error('Error loading materials:', error);
  }
}

$(document).ready(async function () {
  await loadAvailableMaterials();
  cart.loadFromStorage();
  updateQuantity();
  updateHatList(cart.items);
  $('.hatList').on('click', '.count-add', async function () {
    const id = $(this).closest('.hatItem').data('id');
    await cart.addItem(id);
    updateQuantity();
    updateHatList(cart.items);
  });

  $('.hatList').on('click', '.count-remove', function () {
    const id = $(this).closest('.hatItem').data('id');
    cart.removeItem(id);
    updateQuantity();
    updateHatList(cart.items);
  });
  
  $(document).on("input", ".material-quantity", function (e) {
    const materialId = $(this).attr('data-material-id');
    const itemId = $(this).attr('data-item-id');
    const item = cart.getItem(itemId);
    const material = item.materials.find(m => m.materialId == materialId);
    const val = parseInt($(this).val(), 10);
    // Find the item and the material inside the item
    if (material) {
      if (item.hatTypeId != 3) item.hatTypeId = 2
      material.changeQuantity(val)
      cart.syncToStorage();
      updateItemUI(itemId);
    }
  })
  $(document).on("click", ".remove-material", function (e) {
      const materialId = $(this).attr('data-material-id');
      const itemId = $(this).attr('data-item-id');
      // Find the item and the material inside the item
      const item = cart.getItem(itemId);
      if (item) {
        $(this).closest('.material-entry').remove();
        item.removeMaterial(materialId)
        if (item.hatTypeId != 3) item.hatTypeId = 2
        cart.syncToStorage();
        updateQuantity();
        updateItemUI(itemId);
      }
  });
  $(document).on("input", "#item-price", function () {
    const $input = $(this);
    const itemId = $input.closest("li.entry").attr("id");
    const value = parseFloat($input.val()) || 0;
  
    const item = cart.getItem(itemId);
    if (item) {
      item.price = value;
      cart.syncToStorage();
      updateItemUI(itemId);
    }
  });
  $(document).on("click", ".add-material-btn", function (e) {
  
    const $parent = $(this).closest(".entry");
    const itemId = $(this).data('item-id');
    const $dropdown = $(this).siblings('.material-dropdown');
    const $qtyInput = $(this).siblings('.material-add-quantity');
    
    const selectedMaterialId = $dropdown.val();
    const quantity = parseInt($qtyInput.val(), 10);
    
    if (!selectedMaterialId || isNaN(quantity) || quantity <= 0) {
      alert("Välj ett material och ange en korrekt mängd.");
      return;
    }
  
    const selectedMaterial = availableMaterials.find(m => m.materialId == selectedMaterialId);
    if (!selectedMaterial) {
      alert("Material hittades inte.");
      return;
    }
  
    const item = cart.getItem(itemId);
    if (!item) {
      alert("Objektet hittades inte.");
      return;
    }
    const material = new HatMaterial(
      selectedMaterialId, 
      quantity, 
      selectedMaterial.name, 
      selectedMaterial.price, 
      selectedMaterial.unit
    )
    // Add the material to the item's material list
    item.addMaterial(material)
    if (item.hatTypeId != 3) item.hatTypeId = 2
  
    // Save to localStorage
    cart.syncToStorage();

    //Update UI
    const materialHtml = `
      <div class="material-entry" data-material-id="${selectedMaterialId}">
        <p>Material: ${selectedMaterial.name}</p>
        <input 
          type="number" 
          value="${quantity}" 
          class="material-quantity" 
          data-material-id="${selectedMaterialId}" 
          data-item-id="${itemId}" 
          min="0"
        />
        <button class="remove-material" data-material-id="${selectedMaterialId}" data-item-id="${itemId}"> <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0L284.2 0c12.1 0 23.2 6.8 28.6 17.7L320 32l96 0c17.7 0 32 14.3 32 32s-14.3 32-32 32L32 96C14.3 96 0 81.7 0 64S14.3 32 32 32l96 0 7.2-14.3zM32 128l384 0 0 320c0 35.3-28.7 64-64 64L96 512c-35.3 0-64-28.7-64-64l0-320zm96 64c-8.8 0-16 7.2-16 16l0 224c0 8.8 7.2 16 16 16s16-7.2 16-16l0-224c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16l0 224c0 8.8 7.2 16 16 16s16-7.2 16-16l0-224c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16l0 224c0 8.8 7.2 16 16 16s16-7.2 16-16l0-224c0-8.8-7.2-16-16-16z"/></svg></button>
      </div>
    `;
    $parent.find(".settings").append(materialHtml);

  });

  document.querySelectorAll('.hatItem').forEach(hatItem => {
    const counterElement = hatItem.querySelector('.count');
    const id = hatItem.getAttribute('data-id');
    const cartItem = cart.getItem(id)
    if (cartItem) {
      counterElement.textContent = cartItem.quantity;
    }
    else{
      counterElement.textContent = 0;
    }
  });
})

function updateQuantity() {
  document.querySelectorAll('.hatItem').forEach(hatItem => {
    const counterElement = hatItem.querySelector('.count');
    const id = hatItem.getAttribute('data-id');
    const cartItem = cart.getItem(id)
    if (cartItem) {
      counterElement.textContent = cartItem.quantity;
    }
    else{
      counterElement.textContent = 0;
    }
  });
}

function updateItemUI(itemId) {
  const item = cart.getItem(itemId);
  if (!item) return;

  const total = calculateMaterialTotal(item);
  const margin = (item.price - total).toFixed(2);
  console.log(total)

  const $entry = $(`#${itemId}`);
  $entry.find('#total-material-price').text(`Materialpris ${total} kr`);
  $entry.find('#margin').text(`Vinstmarginal: ${margin} kr`);

  item.materials.forEach(material => {
    const matTotal = (material.price * material.quantity).toFixed(2);
    $entry
      .find(`.material-total-price[data-material-id="${material.materialId}"]`)
      .text(`${matTotal} kr`);
  });
}

function updateHatList(cartItems) {
  console.log("updated hat list")
  const itemsEl = $("#orderItems");
  itemsEl.empty();

  cartItems.forEach(item => {
    const html = `
        <li class="entry" id="${item.id}">
          <details>
            <summary>
              <p>${item.name}</p>
              <p>Storlek: ${item.size}</p>
              <p>Längd: ${item.length}</p>
              <p>Bredd: ${item.width}</p>
              <p>Djup: ${item.depth}</p>
              <p> Antal: ${item.quantity} </p>
              <svg id="remove" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                <path d="..."/>
              </svg>
              <p id="icon"></p>
            </summary>

            <div class="settings">
            ${item.materials.map(material => `
              <div class="material-entry" data-material-id="${material.materialId}">
                <p>Material: ${material.name}</p>
                <input 
                  type="number" 
                  value="${material.quantity}" 
                  class="material-quantity" 
                  data-material-id="${material.materialId}" 
                  data-item-id="${item.id}" 
                  min="0"
                />
                <p>${material.unit} </p>
                <p class="material-total-price" data-material-id="${material.materialId}">
  ${(material.price * material.quantity).toFixed(2)} kr
</p>
                <button class="remove-material" data-material-id="${material.materialId}" data-item-id="${item.id}"> <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0L284.2 0c12.1 0 23.2 6.8 28.6 17.7L320 32l96 0c17.7 0 32 14.3 32 32s-14.3 32-32 32L32 96C14.3 96 0 81.7 0 64S14.3 32 32 32l96 0 7.2-14.3zM32 128l384 0 0 320c0 35.3-28.7 64-64 64L96 512c-35.3 0-64-28.7-64-64l0-320zm96 64c-8.8 0-16 7.2-16 16l0 224c0 8.8 7.2 16 16 16s16-7.2 16-16l0-224c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16l0 224c0 8.8 7.2 16 16 16s16-7.2 16-16l0-224c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16l0 224c0 8.8 7.2 16 16 16s16-7.2 16-16l0-224c0-8.8-7.2-16-16-16z"/></svg></button>
              </div>
            `).join('')}
            </div>

            <div class="add-material-form">
              <select class="material-dropdown" data-item-id="${item.id}">
                <option value="">-- Välj material --</option>
                ${availableMaterials.map(mat => `
                  <option value="${mat.materialId}" data-name="${mat.name}" data-unit="${mat.unit}">
                    ${mat.name} (${mat.unit})
                  </option>
                `).join('')}
              </select>
              <input type="number" class="material-add-quantity" placeholder="Mängd" min="1" data-item-id="${item.id}" />
              <button class="add-material-btn" data-item-id="${item.id}">Lägg till material</button>
            </div>
            <p id="total-material-price">Materialpris ${calculateMaterialTotal(item)} kr</p>
            <p id="margin"> Vinstmarginal: ${(item.price - calculateMaterialTotal(item)).toFixed(2)} kr </p>
            <p>Försäljningspris <input id="item-price" type="number" value="${item.price}" /></p>
          </details>
        </li>
      `;
      itemsEl.append(html);
  });
}

function calculateMaterialTotal(item) {
  return (item.materials.reduce((sum, m) => sum + m.price * m.quantity, 0) * item.quantity).toFixed(2);
}

$(document).on('click', '#order-hat-all', function (e) {
    e.preventDefault();
    let userId = $(this).attr("data-userId");
    $(".order-hats-input").val(userId);
});

$(document).ready(function () {
  console.log("document ready");


  $('#orderForm').on('submit', function (e) {
    e.preventDefault(); // prevent normal submit
    const orderData = {
      Customer: {
          Id: $('#CustomerId').val(),
          FirstName: $('#FirstName').val(),
          LastName: $('#LastName').val(),
          HeadMeasurements: parseFloat($('#HeadMeasurements').val()) || 0,
          BillingAddress: $('#BillingAddress').val(),
          DeliveryAddress: $('#DeliveryAddress').val(),
          City: $('#City').val(),
          PostalCode: $('#PostalCode').val(),
          Country: $('#Country').val(),
          Email: $('#Email').val(),
          Phone: $('#Phone').val()
      },
      Hats: cart.items,
      StartDate: $('#StartDate').val(),
      EndDate: $('#EndDate').val(),
      Priority: $('#Priority').is(':checked')
      };
    console.log(orderData)
  
    $.ajax({
      type: "POST",
      url: '/Order/New', // it will use /Order/CreateOrder
      contentType: 'application/json',
      data: JSON.stringify(orderData),
      success: function () {
        cart.clearCart()
        updateHatList(cart.items)
        updateQuantity();
        notify({ type: "success", text: "Ordern skapades" });
      },
    });
  });
});

// Populera formulär med vald kunds uppgifter
document.querySelector('select[name="CustomerId"]').addEventListener('change', function () {
    const customerId = this.value;
    if (customerId > 0) {
        fetch(`/order/get-customer/${customerId}`)
            .then(res => res.json())
            .then(data => {
               
                document.getElementById('FirstName').value = data.firstName;
                document.getElementById('LastName').value = data.lastName;
                document.getElementById('HeadMeasurements').value = data.headMeasurements;
                document.getElementById('BillingAddress').value = data.billingAddress;
                document.getElementById('DeliveryAddress').value = data.deliveryAddress;
                document.getElementById('City').value = data.city;
                document.getElementById('PostalCode').value = data.postalCode;
                document.getElementById('Country').value = data.country;
                document.getElementById('Email').value = data.email;
                document.getElementById('Phone').value = data.phone;
            });
    } else {
        
        ['FirstName', 'LastName', 'HeadMeasurements', 'BillingAddress', 'DeliveryAddress', 'City', 'PostalCode', 'Country','Email','Phone'].forEach(id => {
            document.getElementById(id).value = '';
        });
    }
});


// Kolla om en kund redan är vald vid sidladdning
window.addEventListener('DOMContentLoaded', function () {
    const customerSelect = document.querySelector('select[name="CustomerId"]');
    const customerId = customerSelect.value;

    if (customerId) {
        fetch(`/order/get-customer/${customerId}`)
            .then(res => res.json())
            .then(data => {
                document.getElementById('FirstName').value = data.firstName;
                document.getElementById('LastName').value = data.lastName;
                document.getElementById('HeadMeasurements').value = data.headMeasurements;
                document.getElementById('BillingAddress').value = data.billingAddress;
                document.getElementById('DeliveryAddress').value = data.deliveryAddress;
                document.getElementById('City').value = data.city;
                document.getElementById('PostalCode').value = data.postalCode;
                document.getElementById('Country').value = data.country;
                document.getElementById('Email').value = data.email;
                document.getElementById('Phone').value = data.phone;

            });
    }
});


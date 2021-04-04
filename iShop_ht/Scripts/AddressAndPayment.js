'use strict';
console.log('a');
/*запускает скрипт после того как ДОМ структура сформирована*/
document.addEventListener('DOMContentLoaded', () => {
    //console.log('b');

    const inputTexts = document.querySelector('div.address__block'),
    inputRadioDeliveryMySelfAaP = document.querySelector('input#deliveryMySelfAaP'),
    inputRadioClientYR = document.querySelector('input#customerType2'),
    inputRadioDeliveryAaP = document.querySelector('input#deliveryAaP'),
    selectDost = document.querySelector('select.dostavka__select');



    /*блок доставка. 
    условие: если самовывоз, то скрыть адрес доставки и disabled выбор места выгрузки
    */
    if (inputRadioDeliveryMySelfAaP) {
        inputRadioDeliveryMySelfAaP.addEventListener('click', (event) => {
            
            inputTexts.classList.add("divtextareaNon");
            selectDost.setAttribute("disabled", "disabled");
            
        });
    }
    if (inputRadioDeliveryAaP) {
        inputRadioDeliveryAaP.addEventListener('click', (event) => {

            inputTexts.classList.remove("divtextareaNon");
            selectDost.removeAttribute("disabled");

        });
    }

    /*Блок покутатель
    если Организация, то все поля обязательные и активное делать следующее после заполенения предыдущего
    
    */



});
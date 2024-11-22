//------subopcion de la tabla riesgos------//
function mostrarSubOpciones() {
    const subOpcionesContainer = document.getElementById("subOpcionesContainer");
    const tipoRiesgo = document.getElementById("tipoRiesgo").value;
    const subOpcion = document.getElementById("subOpcion");
    const campoRiesgoCompleto = document.getElementById("campoRiesgoCompleto"); // Agrega el campo al HTML
    subOpcionesContainer.style.display = tipoRiesgo ? "block" : "block";

    // Limpia las opciones anteriores
    subOpcion.innerHTML = '<option value="" disabled selected>Seleccione una subopción</option>';

    // Definición de subopciones por tipo de riesgo
    const opciones = {
        "naturales": [
            { "text": "Cualquier tipo de desastre natural", "value": "desastres" },
            { "text": "Terremotos", "value": "terremotos" },
            { "text": "Inundaciones", "value": "inundaciones" },
            { "text": "Tormentas", "value": "tormentas" },
            { "text": "Incendios", "value": "incendios" }
        ],
        humanas: [
            { text: "Sabotaje", value: "sabotaje" },
            { text: "Robo de información", value: "robo_informacion" },
            { text: "Ataques cibernéticos", value: "ataques_ciberneticos" },
            { text: "Fraude", value: "fraude" },
            { text: "Errores humanos", value: "errores_humanos" },
            { text: "Mala gestión", value: "mala_gestion" }
        ],
        tecnologicas: [
            { text: "Desgaste físico", value: "desgaste_fisico" },
            { text: "Incompatibilidad", value: "incompatibilidad" },
            { text: "Bugs", value: "bugs" },
            { text: "Vulnerabilidades", value: "vulnerabilidades" }
        ],
        organizativas: [
            { text: "Reestructuraciones", value: "reestructuraciones" },
            { text: "Falta de continuidad", value: "falta_continuidad" },
            { text: "Falta de inversión", value: "falta_inversion" },
            { text: "Escasez de personal capacitado", value: "escasez_personal" }
        ],
        externas: [
            { text: "Phishing", value: "phishing" },
            { text: "Malware", value: "malware" },
            { text: "DDoS", value: "ddos" },
            { text: "Espionaje industrial", value: "espionaje_industrial" }
        ],
        internas: [
            { text: "Empleados descontentos", value: "empleados_descontentos" },
            { text: "Acceso no autorizado", value: "acceso_no_autorizado" }
        ],
        entorno: [
            { text: "Nuevas tecnologías", value: "nuevas_tecnologias" },
            { text: "Desactualización", value: "desactualizacion" },
            { text: "Regulaciones", value: "regulaciones" },
            { text: "Cumplimiento", value: "cumplimiento" }
        ]
    };

    // Agrega subopciones al dropdown según el tipo de riesgo seleccionado
    opciones[tipoRiesgo]?.forEach(opcion => {
        const optionElement = document.createElement("option");
        optionElement.value = opcion.value;
        optionElement.text = opcion.text;
        subOpcion.appendChild(optionElement);
    });

    // Actualiza el campo completo cuando cambien las opciones
    subOpcion.onchange = actualizarCampoRiesgo;
    document.getElementById("tipoRiesgo").onchange = actualizarCampoRiesgo;

    // Agrega subopciones al dropdown según el tipo de riesgo seleccionado
    opciones[tipoRiesgo]?.forEach(opcion => {
        const optionElement = document.createElement("option");
        optionElement.value = opcion.value;
        optionElement.text = opcion.text;
        subOpcion.appendChild(optionElement);
    });

    function actualizarCampoRiesgo() {
        const subOpcionSeleccionada = subOpcion.options[subOpcion.selectedIndex];
        campoRiesgoCompleto.value = `${tipoRiesgo} - ${subOpcionSeleccionada ? subOpcionSeleccionada.text : ""}`;
    }

    // Si hay un valor de subopción previo, selecciónalo
    if (Model.SubOpcion) {
        subOpcion.value = Model.SubOpcion; // Asegúrate de tener este valor en el modelo
    }

    // Actualiza el campo completo
    actualizarCampoRiesgo();
}
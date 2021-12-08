import postImposter from './mountebank-helper.js';
import settings from './settings.js';

export default function addService() {
    const USD = { 
        "USD-RUB": {value: 73.60, date: "08.12.2021"},
        "USD-EUR": {value: 0.88, date: "08.12.2021"},
        "USD-GBP": {value: 0.76, date: "08.12.2021"},
        "USD-JPY": {value: 113.66, date: "08.12.2021"},
    }

    const RUB = { 
        "RUB-USD": {value: 0.014, date: "08.12.2021"},
        "RUB-EUR": {value: 0.012, date: "08.12.2021"},
        "RUB-GBP": {value: 0.010, date: "08.12.2021"},
        "RUB-JPY": {value: 1.54, date: "08.12.2021"},
    }

    const EUR = { 
        "EUR-RUB": {value: 83.49, date: "08.12.2021"},
        "EUR-USD": {value: 1.13, date: "08.12.2021"},
        "EUR-GBP": {value: 0.86, date: "08.12.2021"},
        "EUR-JPY": {value: 129.00, date: "08.12.2021"},
    }

    const JPY = { 
        "JPY-RUB": {value: 0.65, date: "08.12.2021"},
        "JPY-USD": {value: 0.0088, date: "08.12.2021"},
        "JPY-GBP": {value: 0.0066, date: "08.12.2021"},
        "JPY-EUR": {value: 0.0078, date: "08.12.2021"},
    }

    const GBP = { 
        "GBP-RUB": {value: 97.40, date: "08.12.2021"},
        "GBP-USD": {value: 1.32, date: "08.12.2021"},
        "GBP-JPY": {value: 150.42, date: "08.12.2021"},
        "GBP-EUR": {value: 1.17, date: "08.12.2021"},
    }

    const stubs = [
        {
            predicates: [ {
                equals: {
                    method: "GET",
                    "path": "/USD"
                }
            }],
            responses: [
                {
                    is: {
                        statusCode: 200,
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify(USD)
                    }
                }
            ]
        },
        {
            predicates: [ {
                equals: {
                    method: "GET",
                    "path": "/RUB"
                }
            }],
            responses: [
                {
                    is: {
                        statusCode: 200,
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify(RUB)
                    }
                }
            ]
        },
        {
            predicates: [ {
                equals: {
                    method: "GET",
                    "path": "/EUR"
                }
            }],
            responses: [
                {
                    is: {
                        statusCode: 200,
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify(EUR)
                    }
                }
            ]
        },
        {
            predicates: [ {
                equals: {
                    method: "GET",
                    "path": "/JPY"
                }
            }],
            responses: [
                {
                    is: {
                        statusCode: 200,
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify(JPY)
                    }
                }
            ]
        },
        {
            predicates: [ {
                equals: {
                    method: "GET",
                    "path": "/GBP"
                }
            }],
            responses: [
                {
                    is: {
                        statusCode: 200,
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify(GBP)
                    }
                }
            ]
        }
    ];

    const imposter = {
        port: settings.currency_service_port,
        protocol: 'http',
        stubs: stubs
    };

    return postImposter(imposter);
}

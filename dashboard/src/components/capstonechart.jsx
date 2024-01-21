import React from "react";
import { Line } from "react-chartjs-2";
import { 
    Chart,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
} from "chart.js";
import { 
    Container,
    Row,
    Col
} from "react-bootstrap";

function CapstoneChart ({data}) {
    const chartData = data.map(item => 
        (
            {  
                Temperature: item.Temperature,
                Humidity: item.Humidity,
                TimeStamp: item.TimeStamp
            }
        )
    );

    const labels = chartData.map (item => item.TimeStamp);
    
    Chart.register (
        CategoryScale, 
        LineElement, 
        LinearScale, 
        PointElement,
        Title,
        Tooltip,
        Legend
    );

    const options = {
        reponsive: true,
        plugins: {
            legend: {
                position: 'top'
            },
            title: {
                display: true,
                text: 'Sensors chart'
            }
        }
    }

    const temperatureSerie = {
        labels,
        datasets: [
            {
                label: 'Temperature',
                data: chartData.map(item => item.Temperature),
                borderColor: 'rgb(255, 99, 132)',
                backgroundColor: 'rgba(255,99,132,0.5)'
            }
        ]
    };

    const humiditySerie = {
        labels,
        datasets: [
            {
                label: 'Humidity',
                data: chartData.map(item => item.Humidity),
                borderColor: 'rgb(53,162,235)',
                backgroundColor: 'rgba(53,162,235,0.5)'
            }
        ]
    }

    return (
        <Container>
            <Row>
                <Col>
                    <Line options={options} data={temperatureSerie} />
                </Col>
                <Col>
                    <Line options={options} data={humiditySerie} />
                </Col>
            </Row>
        </Container>
    );
}

export default CapstoneChart;
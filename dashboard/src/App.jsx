import { Suspense, useEffect, useState } from 'react'
import { getDataByDates } from './store/data'
import { Container, Form, Row, Col } from 'react-bootstrap'
import CapstoneChart from './components/capstonechart'

function App () {
    const [data, setData] = useState([]);
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');
    
    const fetchData = async (start, end, summarize) => {
        let response = await getDataByDates (start, end, summarize);
        setData (response);
    };

    useEffect (() => {
        let sdParam = '';
        if (startDate === 'undefined' || startDate === '') {
            let date = new Date ();
            date.setDate (date.getDate ());
            date.setMonth (date.getMonth() - 1);
            sdParam = date.toISOString().slice (0,10)
            setStartDate (sdParam);
        } else {
            sdParam = startDate;
        }
        
        let edParam = '';
        if (endDate === 'undefined' || endDate === '') {
            edParam = new Date().toISOString().slice (0, 10)
            setEndDate (edParam);
        } else {
            edParam = endDate;
        }

        fetchData (sdParam, edParam, true);
    }, [startDate, endDate]);

    const onChange = (e) => {
        if (e.target.id === 'calendar_start_date') {
            let start = e.target.value;
            setStartDate (start);
        }

        if (e.target.id === 'calendar_end_date') {
            let end = e.target.value;
            setEndDate (end);
        }
    }

    return (
        <Suspense>
        <Container>
            <Row>
                <Col>
                    Date Start
                </Col>
                <Col>
                    <Form.Control id='calendar_start_date' type="date" value={startDate} onChange={onChange}/>
                </Col>
                <Col>
                    Date End
                </Col>
                <Col>
                    <Form.Control id='calendar_end_date' type="date" value={endDate} onChange={onChange}/>
                </Col>                
            </Row>
            <Row>
                <Col>
                    <CapstoneChart
                        title='Engine 1'
                        legend='Temperature'
                        labels={data.map(item => new Date (item.date).toLocaleDateString("en-US"))}
                        serie={data.map(item => item.temperature)}
                        color='rgb(255, 99, 132)'
                        backgroundColor='rgba(255,99,132,0.5)'
                    />
                </Col>
                <Col>
                    <CapstoneChart
                        title='Engine 1'
                        legend='Humidity'
                        labels={data.map(item => new Date(item.date).toLocaleDateString("en-US"))}
                        serie={data.map(item => item.humidity)}
                        color='rgb(53,162,235)'
                        backgroundColor='rgba(53,162,235,0.5)'
                    />
                </Col>
            </Row>
        </Container>
        </Suspense>
    )
}

export default App

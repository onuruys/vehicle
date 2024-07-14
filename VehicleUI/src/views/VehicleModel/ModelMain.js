import React, { useEffect, useState } from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
// core components
import GridItem from "components/Grid/GridItem.js";
import GridContainer from "components/Grid/GridContainer.js";
import Card from "components/Card/Card.js";
import CardHeader from "components/Card/CardHeader.js";
import CardBody from "components/Card/CardBody.js";
import axios from "axios";
import config from "../../config.js";
import { Button } from "@material-ui/core";
import DataTable from "components/Table/DataTable.js";
import ModelModal from "./ModelModal.js";
const styles = {
  cardCategoryWhite: {
    "&,& a,& a:hover,& a:focus": {
      color: "rgba(255,255,255,.62)",
      margin: "0",
      fontSize: "14px",
      marginTop: "0",
      marginBottom: "0",
    },
    "& a,& a:hover,& a:focus": {
      color: "#FFFFFF",
    },
  },
  cardTitleWhite: {
    color: "#FFFFFF",
    marginTop: "0px",
    minHeight: "auto",
    fontWeight: "300",
    fontFamily: "'Roboto', 'Helvetica', 'Arial', sans-serif",
    marginBottom: "3px",
    textDecoration: "none",
    "& small": {
      color: "#777",
      fontSize: "65%",
      fontWeight: "400",
      lineHeight: "1",
    },
  },
};

const useStyles = makeStyles(styles);

export default function ModelMain() {
  const [modelData, setModelData] = useState([]);
  const [brands, setBrands] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [editedModel, setEditedModel] = useState(undefined);
  const [removeSelected, setRemoveSelected] = useState(false);

  //#region Actions
  const getBrands = () => {
    axios({
      method: "get",
      url: `${config.DefaultApiUrl}/Brand/lov`,
      transformResponse: [
        (data) => {
          const res = JSON.parse(data);
          setBrands(res);
        },
      ],
    });
  };

  const getModelListAction = () => {
    axios({
      method: "post",
      url: `${config.DefaultApiUrl}/Model/all`,
      data: {},
      transformResponse: [
        (data) => {
          setModelData(JSON.parse(data));
          setRemoveSelected(false);
        },
      ],
    });
  };

  const addOrUpdateModelActions = (data) => {
    const method = data.ModelId ? "put" : "post";
    axios({
      method: method,
      url: `${config.DefaultApiUrl}/model`,
      data: data,
      transformResponse: [
        () => {
          setShowModal(false);
          alert("Kayıt işlemi tamamlanmıştır");
          getModelListAction();
        },
      ],
    });
  };

  const deleteModelActions = (data) => {
    axios({
      method: "delete",
      url: `${config.DefaultApiUrl}/model/${data[0]}`,
      transformResponse: [
        () => {
          alert("Silme işlemi tamamlanmıştır");
          setRemoveSelected(true);
          getModelListAction();
        },
      ],
    });
  };

  useEffect(() => {
    getModelListAction();
    getBrands();
  }, []);

  const newModel = () => {
    setEditedModel({
      Active: true,
      ModelName: "",
      BrandId: undefined,
      ModelId: undefined,
    });
    setShowModal(true);
  };

  const cells = [
    {
      id: "ModelName",
      numeric: false,
      disablePadding: true,
      label: "Model Name",
      dataType: "text",
    },
    {
      id: "BrandId",
      // numeric: true,
      label: "Brand Id",
      dataType: "number",
    },
    {
      id: "Active",
      numeric: false,
      label: "Is Active",
      dataType: "boolean",
    },
  ];

  const classes = useStyles();
  return (
    <GridContainer>
      <GridItem xs={12} sm={12} md={12}>
        <Card>
          <CardHeader color="primary">
            <p>
              Click to add a <Button onClick={newModel}>New Model</Button>
            </p>
            <h4 className={classes.cardTitleWhite}>Vehicle Model Management</h4>
          </CardHeader>
          <CardBody>
            {modelData && (
              <DataTable
                headCells={cells}
                data={modelData}
                primaryId="ModelId"
                handleEdit={(rowData) => {
                  setEditedModel(rowData);
                  setShowModal(true);
                }}
                handleDelete={(rows) => {
                  deleteModelActions(rows);
                }}
                removeSelected={removeSelected}
              />
            )}
          </CardBody>
        </Card>
      </GridItem>
      <CardBody>
        {showModal && (
          <ModelModal
            open={showModal}
            detail={editedModel}
            handleClose={() => setShowModal(false)}
            addOrUpdateModel={(data) => addOrUpdateModelActions(data)}
            brands={brands}
          />
        )}
      </CardBody>
    </GridContainer>
  );
}
